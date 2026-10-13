using UrlShortener.Middleware;
using UrlShortener.Repositories;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUrlRepository, UrlRepository>();

builder.Services.AddScoped<IUrlService, UrlService>();

builder.Services.AddScoped<ILogService, LogService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();