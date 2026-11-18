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

            // Conexión a MySQL
            var connectionString = builder.Configuration
                .GetConnectionString("ConnectionMySql");

            builder.Services.AddDbContext<SocialMediaContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)));

            // Repositorios y servicios
            builder.Services.AddTransient<IPostService, PostService>();

            builder.Services.AddScoped(
                typeof(IBaseRepository<>),
                typeof(BaseRepository<>));

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddSingleton<
                IDbConnectionFactory,
                DbConnectionFactory>();

            builder.Services.AddScoped<IDapperContext, DapperContext>();

            // Reportería
            builder.Services.AddTransient<
                IReportesService,
                ReportesService>();

            builder.Services.AddTransient<
                IReportesRepository,
                ReportesRepository>();

            // Controladores
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            builder.Services.AddOpenApi();

            // AutoMapper: registra los perfiles del ensamblado
            builder.Services.AddAutoMapper(typeof(PostProfile).Assembly);

            // Validadores
            builder.Services.AddScoped<PostDtoValidator>();
            builder.Services.AddScoped<CrearPostDtoValidator>();
            builder.Services.AddScoped<ActualizarPostDtoValidator>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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