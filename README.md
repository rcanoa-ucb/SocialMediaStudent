# SocialMedia desde cero — .NET 9 y MySQL

Este proyecto implementa la parte central de la guía del docente: arquitectura Core, Infrastructure, Services y API; modelos User/Post/Comment; EF Core con MySQL; repositorio genérico y Unit of Work; DTO, AutoMapper, FluentValidation; controladores CRUD de Post y registro/listado de User.

## Requisitos
- .NET SDK 9
- MySQL 8 instalado y en ejecución
- Visual Studio o VS Code con extensión C#

## 1. Crear la base de datos
Ejecuta en MySQL Workbench:
```sql
CREATE DATABASE IF NOT EXISTS DbSocialMedia CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

## 2. Modificar contraseña
Cambia CAMBIA_TU_PASSWORD en `SocialMedia.API/appsettings.json` por la contraseña local de MySQL. No subas contraseñas a Git; para un repositorio compartido, usa User Secrets o variables de entorno.

## 3. Generar la solución
Desde la carpeta principal:
```powershell
dotnet new sln -n SocialMedia
dotnet sln add SocialMedia.Core/SocialMedia.Core.csproj
dotnet sln add SocialMedia.Infrastructure/SocialMedia.Infrastructure.csproj
dotnet sln add SocialMedia.Services/SocialMedia.Services.csproj
dotnet sln add SocialMedia.API/SocialMedia.API.csproj
dotnet restore
```

## 4. Crear la base con migraciones
```powershell
dotnet tool install --global dotnet-ef --version 9.0.10
dotnet ef migrations add InitialCreate --project SocialMedia.Infrastructure --startup-project SocialMedia.API --output-dir Data/Migrations
dotnet ef database update --project SocialMedia.Infrastructure --startup-project SocialMedia.API
```
Si `dotnet-ef` ya está instalado, sustituye la instalación por `dotnet tool update --global dotnet-ef --version 9.0.10`.

## 5. Ejecutar
```powershell
dotnet run --project SocialMedia.API --launch-profile http
```
Abrir http://localhost:5055/swagger

## 6. Probar primero crear usuario
POST `/api/User`:
```json
{"firstName":"Ana","lastName":"Perez","email":"ana@example.com","dateOfBirth":"2000-01-01","telephone":"77712345"}
```
Anota el `id` generado.

## 7. Crear post
POST `/api/Post`:
```json
{"userId":1,"date":"2026-11-18","description":"Mi primera publicación de prueba","image":null}
```
GET `/api/Post`, GET `/api/Post/1`, PUT `/api/Post/1` y DELETE `/api/Post/1`.

## Correspondencia con el PDF
El material de 74 páginas también incluye ejercicios JSON iniciales, versiones sin DTO, reglas adicionales, Dapper y JWT. Este repositorio es una base **funcional central**, no una reproducción íntegra de todas las variantes históricas del PDF. No hay autenticación JWT aún ni reportes Dapper. Las reglas de negocio del documento deben ajustarse según los requerimientos exactos del docente.

## Git
Trabaja en tu rama, verifica con `git status` y no subas credenciales. En un repositorio de equipo, copia solo los archivos necesarios, sin borrar contenido preexistente.

