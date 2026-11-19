using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authentication(UserLogin userLogin)
        {
            //Si es un usuario válido
            var token = GenerateToken(userLogin);
            return Ok(new { token });
        }

        private string GenerateToken(UserLogin userLogin)
        {
            //HEADER
            /*
             * Crea la clave simétrica
             * _configuration["Authentication:SecretKey"] → Lee la clave secreta (ej: "miSuperSecretoLargo123!").
             * Encoding.UTF8.GetBytes(...) → Convierte el string en un arreglo de bytes.
             * new SymmetricSecurityKey(...) → Crea una clave que se usará para firmar el token
             * Es la misma clave que usará el servidor para validar el token después.
             */
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));

            /*Crea las credenciales de firma
             * SigningCredentials → Objeto que contiene la clave y el algoritmo.
             * SecurityAlgorithms.HmacSha256 → Algoritmo de firma (el más común para JWT).
             * efine cómo se va a firmar el token.
             */
            var signingCredentials =
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            /* Crea el encabezado del JWT
             * JwtHeader → Contiene metadatos como:
             * alg: "HS256" (algoritmo de firma)
             * typ: "JWT"
             * Esta parte se codifica en Base64 y será la primera sección del token.
             */
            var header = new JwtHeader(signingCredentials);

            //CLAIMS (CUERPO)
            /* Estos datos son visibles para cualquiera que decodifique el token (solo están en Base64, no encriptados).
             * No ESCRIBIR contraseñas o datos sensibles aquí.
             */
            var claims = new[]
            {
                new Claim("Name", "Juan Perez"),
                new Claim(ClaimTypes.Email, "jperez@ucb.edu.bo"),
                new Claim(ClaimTypes.Role, "Administrador"),
            };

            //PAYLOAD
            var payload = new JwtPayload(
                //Quién emite el token (ej: "https://api.ucb.edu.bo").
                issuer: _configuration["Authentication:Issuer"],
                //Quién va a recibir el token (ej: "https://frontend.ucb.edu.bo")
                audience: _configuration["Authentication:Audience"],
                //Los datos del usuario.
                claims: claims,
                //Desde cuándo es válido (ahora mismo).
                notBefore: DateTime.UtcNow,
                //Cuándo expira (en 2 minutos).
                expires: DateTime.UtcNow.AddMinutes(2)
            );

            //Generar el token JWT
            /* Combina header + payload en un objeto JWT
             * JwtSecurityToken → Objeto que contiene el token sin firmar todavía.
             * aquí se unen las dos primeras partes (encabezado y cuerpo).            
             */
            var token = new JwtSecurityToken(header, payload);

            //Serializar el token
            /* Firma y convierte el token a string
             * JwtSecurityTokenHandler → Clase que sabe manejar JWT.
             * WriteToken(token) → Firma el token usando las credenciales y lo convierte en el string final con el formato:
             */
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
