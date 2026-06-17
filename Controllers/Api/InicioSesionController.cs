using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProyectoExpedientePacientes.Data;
using ProyectoExpedientePacientes.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoExpedientePacientes.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public InicioSesionController(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var usuario = _context.Usuario
                .FirstOrDefault(p =>
                    p.Correo == dto.Correo &&
                    p.Contrasenia == dto.Contrasenia);

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Credenciales incorrectas"
                });
            }

            var token = GenerarToken(usuario);

            return Ok(new
            {
                token,
                usuario.PacienteId
            });
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(
                ClaimTypes.NameIdentifier,
                usuario.PacienteId.ToString()),

            new Claim(
                ClaimTypes.Email,
                usuario.Correo)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

    }
}
