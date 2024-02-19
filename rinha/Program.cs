using Microsoft.AspNetCore.Http.Timeouts;
using rinha.Controllers;
using rinha.Infrastruture;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("HTTP_PORT") ?? "9999";


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDBContext>();

builder.Services.AddRequestTimeouts(options => options.DefaultPolicy = new RequestTimeoutPolicy { Timeout = TimeSpan.FromSeconds(60) });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapClientesRoutes();


app.Run($"http://localhost:{port}");
