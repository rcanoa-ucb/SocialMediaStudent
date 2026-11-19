using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Services.Interfaces;
using SocialMedia.Services.Mappings;
using SocialMedia.Services.Services;
using SocialMedia.Services.Validators;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("ConnectionMySql")
    ?? throw new InvalidOperationException("Falta ConnectionMySql en appsettings.json");
builder.Services.AddDbContext<SocialMediaContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IValidator<PostDto>, PostDtoValidator>();
builder.Services.AddAutoMapper(typeof(PostProfile).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
