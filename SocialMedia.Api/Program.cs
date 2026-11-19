
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
            builder.Services.AddDbContext<SocialMediaContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            #endregion

            // Add services to the container.
            //builder.Services.AddTransient<IPostRepository, PostRepository>();
            //builder.Services.AddTransient<ICommentRepository, CommentRepository>();
            builder.Services.AddTransient<IPostService, PostService>();
            //builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IDapperContext, DapperContext>();
            builder.Services.AddTransient<IReportesService, ReportesService>();
            builder.Services.AddTransient<IReportesRepository, ReportesRepository>();

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //Configurar JWT
            builder.Services.AddAuthentication(options =>
            {
                /*Esquema por defecto para autenticar (identificar quién es el usuario).
                 * DefaultAuthenticateScheme → Esquema por defecto para autenticar (identificar quién es el usuario).
                 * JwtBearerDefaults.AuthenticationScheme → Equivale al string "Bearer".
                 * Significado: "Cuando llegue una petición, usa JWT Bearer para identificar al usuario"
                 * */
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                /*DefaultChallengeScheme → Esquema por defecto cuando se desafía al usuario (por ejemplo, 
                 * si intenta acceder a algo sin estar autenticado).
                 * Si no hay token o es inválido, el sistema responderá con un desafío típico de JWT (401 Unauthorized)               
                 * */
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    //TokenValidationParameters → Objeto que contiene todas las reglas de validación para un token JWT.
                    new TokenValidationParameters
                    {
                        //Valida el emisor (iss) → Verifica que el token haya sido emitido por un servidor de confianza. Evita que alguien use tokens creados por otro sistema.
                        ValidateIssuer = true,
                        //Verifica que el token esté dirigido a esta API. Evita que un token para otro servicio(ej: un frontend) sea aceptado aquí.
                        ValidateAudience = true,
                        //Comprueba que el token no haya expirado y que su "no válido antes de" sea correcto. Es la razón por la que los tokens dejan de funcionar automáticamente después de X tiempo.
                        ValidateLifetime = true,
                        //Verifica que el token no haya sido modificado. Usa la clave secreta para asegurar que el token es genuino y no manipulado.
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        /*SymmetricSecurityKey → Clave simétrica (misma clave para firmar y verificar).
                         * Encoding.UTF8.GetBytes(...) → Convierte la clave(string) en bytes.
                         * builder.Configuration["Authentication:SecretKey"] → Lee la clave desde appsettings.json(ej: "miSuperSecretoLargo123!").
                        */
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(
                                builder.Configuration["Authentication:SecretKey"]
                            )
                        )
                    };
            });


            builder.Services.AddAutoMapper(typeof(PostProfile).Assembly);

            //Registrar Validadores de FluentValidator
            builder.Services.AddScoped<PostDtoValidator>();
            builder.Services.AddScoped<CrearPostDtoValidator>();
            builder.Services.AddScoped<ActualizarPostDtoValidator>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}