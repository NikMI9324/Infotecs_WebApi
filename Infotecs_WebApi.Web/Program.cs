using Infotecs_WebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infotecs_WebApi.Infrastructure.Interfaces;
using Infotecs_WebApi.Infrastructure.Repository;
using Infotecs_WebApi.Application.Interfaces;
using Infotecs_WebApi.Application.Services;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<InfotecsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    x => x.MigrationsAssembly("Infotecs_WebApi.Infrastructure")));
builder.Services.AddScoped<IDataProcessingService, DataProcessingService>();
builder.Services.AddScoped<FilterCollection>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IValueRepository, ValueRepository>();
builder.Services.AddScoped<IResultQueryService, ResultQueryService>();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");


app.MapControllers();

app.Run();
