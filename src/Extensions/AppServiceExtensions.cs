using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerlaMetroApiMain.Middlewares;
using PerlaMetroApiMain.Services;
using PerlaMetroApiMain.Services.Interfaces;
using PerlaMetroApiMain.Util;

namespace PerlaMetroApiMain.Extensions;

/// <summary>
/// Extension methods for configuring the web application and its services.
/// </summary>
public static class AppServiceExtensions
{
    public const string AllowAllCorsPolicy = "AllowAll";
    public const string RestrictedCorsPolicy = "Restricted";

    /// <summary>
    /// Configures MVC, CORS, and output caching for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="config">The application configuration.</param>
    public static void AddWebApp(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddControllers(options =>
            {
                options.Conventions.Add(
                    new RouteTokenTransformerConvention(new LowercaseParameterTransformer()));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problem = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation failed",
                        Detail = "One or more validation errors occurred.",
                        Instance = context.HttpContext.Request.Path,
                        Type = "https://httpstatuses.io/400"
                    };
                    problem.Extensions["traceId"] = context.HttpContext.TraceIdentifier;

                    return new BadRequestObjectResult(problem)
                    {
                        ContentTypes = { "application/json" }
                    };
                };
            });

        services.AddOutputCache(options =>
        {
            options.AddBasePolicy(builder => builder.NoCache());
        });

        AddCorsPolicies(services, config);
    }

    /// <summary>
    /// Configures the middleware pipeline for the web application.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    /// <returns>The configured web application.</returns>
    public static WebApplication UseWebApp(this WebApplication app)
    {
        app.UseOutputCache();

        app.UseProblemDetailsExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseCors(AllowAllCorsPolicy);
        }
        else
        {
            app.UseCors(RestrictedCorsPolicy);
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    /// <summary>
    /// Registers application services and custom services.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        AddServices(services);
        AddOpenApiMapper(services);
        AddAuthentication(services, config);
        services.AddAuthorization();
        AddHttpContextAccessor(services);
    }

    /// <summary>
    /// Registers custom application services.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    private static void AddServices(IServiceCollection services)
    {
        services.AddHttpClient<IUsersService, UsersService>();
        services.AddHttpClient<IStationService, StationService>();
    }

    /// <summary>
    /// Configures OpenAPI and Scalar API documentation services.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    private static void AddOpenApiMapper(IServiceCollection services)
    {
        // Explorer and OpenAPI document generation
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    /// <summary>
    /// Configures CORS policies for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="config">The application configuration.</param>
    private static void AddCorsPolicies(IServiceCollection services, IConfiguration config)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowAllCorsPolicy, policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });

            var origins = config.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
            if (origins.Length == 0)
            {
                // Provide a conservative default to avoid failures in prod if not configured
                origins = ["http://localhost", "http://127.0.0.1"];
            }

            options.AddPolicy(RestrictedCorsPolicy, policy =>
            {
                policy.WithOrigins(origins)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
    }

    private static IServiceCollection AddAuthentication(IServiceCollection services, IConfiguration config)
    {
        var jwtSecret = config.GetValue<string>("JWT_SECRET") ?? throw new InvalidOperationException("JWT_SECRET is not configured.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        return services;
    }

    /// <summary>
    /// Registers the HTTP context accessor service.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    private static void AddHttpContextAccessor(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
    }
}
