using AutoMapper;
using FluentAssertions.Common;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Services;
using Questao5.Business.Configuration;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Questao5.Business.Configuration;
using FluentValidation;
using Questao5.Application.Validation.PipelineBehaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); 

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRegister(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

//builder.Services.AddScoped<IRegisterService, RegisterTypes>();
//builder.Services.AddScoped<IContaCorrenteMovimentoService, ContaCorrenteMovimentoService>();
//builder.Services.AddScoped<IContaCorrenteMovimentoRepository, ContaCorrenteMovimentoRespository>();


//builder.Services.AddTransient<ContaCorrenteMovimentoService>();
//builder.Services.AddTransient<ContaCorrenteMovimentoRespository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


