using PerlaMetroApiMain.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddWebApp(builder.Configuration);

var app = builder.Build();

app.UseWebApp();

app.Run();
