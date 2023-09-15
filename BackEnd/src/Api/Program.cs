using Api.Mediator;
using Data;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BackEnd",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Londoño Ospina",
                        Email = "londonoospinadaniel@gmail.com"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

Console.WriteLine("Adding DB");
builder.Services.AddDbContext<Context>(opt =>
{
    opt.UseInMemoryDatabase("Database");
});


Console.WriteLine("Adding AutoMapper");
builder.Services.AddAutoMapper(Assembly.Load("Services"));

Console.WriteLine("Adding MediatR");
builder.Services.AddMediatRConf();

Console.WriteLine("Adding DI");
builder.Services.AddScoped<IContext, Context>();

Console.WriteLine("Enabling CORS");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEverything", // This is the open house we talked about!
        builder =>
        {
            builder.AllowAnyOrigin() // Any origin is welcome...
                .AllowAnyHeader() // With any type of headers...
                .AllowAnyMethod(); // And any HTTP methods. Such a jolly party indeed!
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowEverything");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
