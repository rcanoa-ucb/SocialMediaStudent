
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Infrastructure.Mappings;

namespace SocialMedia.Api
{
    public class Program
    {
        // Los repositorios son accesos a datos , al ser repositorios se transforman en una base de datos , en este esque funciona con cualquier base de datos
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configurar la BD MySql
            var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
            builder.Services.AddDbContext<SocialMediaContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            #endregion
            builder.Services.AddTransient<IPostRepository,PostRepository>();
            builder.Services.AddTransient<ICommentRepository,CommentRepository>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddAutoMapper(typeof(PostProfile).Assembly);

            var app = builder.Build();

            // Asegurar que la base de datos exista al arrancar (crea la BD y tablas si no existen)
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<SocialMediaContext>();
                    db.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, lo registramos en la consola para diagnóstico.
                    Console.WriteLine($"Error asegurando la base de datos: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
