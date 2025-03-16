using IActionResultExemplo.application.service;
using IActionResultExemplo.domain.models;
using IActionResultExemplo.domain.repository;
using IActionResultExemplo.exception;
using IActionResultExemplo.infrastructure.repository;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Npgsql; // Usando Npgsql para PostgreSQL

namespace IActionResultExemplo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona os controllers
            builder.Services.AddControllers();

            // Registra IDbConnection para PostgreSQL
            builder.Services.AddScoped<IDbConnection>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new NpgsqlConnection(connectionString);
            });

            // Registra os serviços
            builder.Services.AddScoped<IAtendenteService, AtendenteService>();
            builder.Services.AddScoped<IAtendenteRepository, AtendenteRepository>();
            builder.Services.AddScoped<IPasswordHasher<Atendente>, PasswordHasher<Atendente>>();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>(); // UseMiddleware handles the RequestDelegate dependency
            app.MapControllers();
            app.Run();
        }
    }
}
