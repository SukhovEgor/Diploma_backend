using Application.DTOs;
using Application.Interfaces;
using Application.UseCases;
using Infrastructure.DAL;
using Infrastructure.DAL.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Server;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var assembly = Assembly.GetAssembly(typeof(MappingProfile));
builder.Services.AddAutoMapper(assembly);

builder.Services.AddScoped<ICalculationResultRepository, CalculationResultRepository>();
builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddScoped<ICalculationModule, CalculationModule>();
builder.Services.AddScoped<IResultProcessService, ResultProcessService>();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection), ServiceLifetime.Transient);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseCors(builder => builder.AllowAnyMethod().WithOrigins("http://localhost:3000").AllowAnyHeader().AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
