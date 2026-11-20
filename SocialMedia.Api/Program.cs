
using Microsoft.EntityFrameworkCore;
using SocialMedia.Api.Filters;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Mappings;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Services.Interfaces;
using SocialMedia.Services.Services;
using SocialMedia.Services.Validators;

namespace SocialMedia.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configurar la BD MySql

            var connectionString = builder.Configuration
                .GetConnectionString("ConnectionMySql");

            builder.Services.AddDbContext<SocialMediaContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                ));

            #endregion

            #region Registrar Servicios

            // Servicios
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddScoped<IReportesService, ReportesService>();

            #endregion

            #region Registrar Repositorios

            builder.Services.AddScoped(
                typeof(IBaseRepository<>),
                typeof(BaseRepository<>)
            );

            #endregion

            #region UnitOfWork

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Configurar Dapper

            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IDapperContext, DapperContext>();

            #endregion

            #region Controllers

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            #endregion

            // OpenAPI
            builder.Services.AddOpenApi();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(PostProfile).Assembly);

            #region Validadores

            builder.Services.AddScoped<PostDtoValidator>();
            builder.Services.AddScoped<CrearPostDtoValidator>();
            builder.Services.AddScoped<ActualizarPostDtoValidator>();

            #endregion

            var app = builder.Build();

            // Middleware de excepciones
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configurar OpenAPI
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
