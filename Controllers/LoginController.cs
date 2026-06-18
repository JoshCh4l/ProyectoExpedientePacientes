using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Models;
using ProyectoExpedientePacientes.Data;
using System.Security.Claims;

namespace ProyectoExpedientePacientes.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string correo, string contrasenia)
        {

            var usuario = await _context.Usuario
        .FirstOrDefaultAsync(u =>
            u.Correo == correo &&
            u.Contrasenia == contrasenia);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o Contrasenia Incorrectos ";
                return View();

            }

            if (usuario.Bloqueado)
            {
                ViewBag.Error = "Usuario bloqueado";
                return View();

            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString()),

        };

            if (usuario.MedicoId.HasValue)
            {
                claims.Add(new Claim("MedicoId", usuario.MedicoId.Value.ToString()));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            if (usuario.Rol == Roles.Medico)
            {
                return RedirectToAction("Index", "Paciente");
            }

            return RedirectToAction("Index", "Home");
        }

        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }

    }

}
