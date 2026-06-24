using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var usuario = _context.Usuario
                .FirstOrDefault(p =>
                    p.Correo == dto.Correo &&
                    p.Contrasenia == dto.Contrasenia);

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Usuario o Contrasenia Incorrectos"
                });
            }

            if (usuario.Bloqueado)
            {
                return Unauthorized(new
                {
                    mensaje = "Usuario Bloqueado"
                });
            }

            var token = GenerarToken(usuario);

            return Ok(new
            {
                token,
                usuario.PacienteId
            });
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] Usuario model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool existeCorreo = await _context.Usuario
                .AnyAsync(u => u.Correo == model.Correo);

            if (existeCorreo)
                return BadRequest("Ya existe un usuario con ese correo");

            bool existeCedula = await _context.Usuario
                .AnyAsync(u => u.Cedula == model.Cedula);

            if (existeCedula)
                return BadRequest("Ya existe un usuario con esa cedula");

            var paciente = new Paciente();

            _context.Paciente.Add(paciente);
            await _context.SaveChangesAsync();

            var usuario = new Usuario
            {
                Cedula = model.Cedula,
                Nombre = model.Nombre,
                Correo = model.Correo,
                Contrasenia = model.Contrasenia, 
                Rol = Roles.Paciente,
                Bloqueado = false,
                PacienteId = paciente.Id
            };

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario registrado correctamente"
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
