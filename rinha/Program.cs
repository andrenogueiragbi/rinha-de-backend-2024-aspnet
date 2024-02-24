using rinha.Controllers;
using rinha.Infrastruture;


var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("HTTP_PORT") ?? "8080";

builder.Services.AddScoped<AppDBContext>();

var app = builder.Build();

app.MapClientesRoutes();

app.Run($"http://localhost:{port}");



