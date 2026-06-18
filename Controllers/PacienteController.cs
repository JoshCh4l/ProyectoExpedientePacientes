using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Data;
using ProyectoExpedientePacientes.Models;

namespace ProyectoExpedientePacientes.Controllers
{
    [Authorize(Roles = "Administrador,Medico")]
    public class PacienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PacienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pacientes = await _context.Usuario
            .Where(u => u.PacienteId != null)
            .Select(u => new ModeloVistaPaciente
            {
                PacienteId = u.PacienteId!.Value,
                Nombre = u.Nombre,
                Cedula = u.Cedula,
                Correo = u.Correo,

                UltimaAtencion = _context.NotaClinica
                    .Where(n => n.PacienteId == u.PacienteId)
                    .Max(n => (DateTime?)n.FechaRegistro)
            })
            .OrderByDescending(p => p.UltimaAtencion)
            .ToListAsync();

            return View(pacientes);
        }

        public async Task<IActionResult> Historial(int id)
        {
            var historial = await _context.NotaClinica
                .Where(n => n.PacienteId == id)
                .Include(n => n.Usuario)
                .OrderByDescending(n => n.FechaRegistro)
                .ToListAsync();

            ViewBag.PacienteId = id;

            return View(historial);

        }

        [HttpGet]
        public async Task<IActionResult> AgregarNota(int id)
        {
            ViewBag.PacienteId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarNota(int id, string contenido)
        {
            var usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

            var nota = new NotaClinica
            {
                PacienteId = id,
                UsuarioId = usuarioId,
                Contenido = contenido,
                FechaRegistro = DateTime.Now
            };

            _context.NotaClinica.Add(nota);
            await _context.SaveChangesAsync();
            return RedirectToAction("Historial", new { id });

        }


    }
}
