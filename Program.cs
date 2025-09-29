//using PerlaMetroApiMain.Services;
//using PerlaMetroApiMain.Services.Interfaces;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddHttpClient();
//builder.Services.AddScoped<IRouteService, RouteService>();


// using PerlaMetroApiMain.Services;
// using PerlaMetroApiMain.Services.Interfaces;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddControllers();

// builder.Services.AddHttpClient();


// builder.Services.AddScoped<IStationService, StationService>();


// var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

using PerlaMetroApiMain.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Bind to PORT env var if provided (e.g., Render)
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddWebApp(builder.Configuration);

var app = builder.Build();

app.UseWebApp();
app.Run();
