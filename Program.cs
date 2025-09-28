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

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddWebApp(builder.Configuration);

var app = builder.Build();

app.UseWebApp();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
