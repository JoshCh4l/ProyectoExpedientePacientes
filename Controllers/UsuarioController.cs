using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Data;
using ProyectoExpedientePacientes.Models;

namespace ProyectoExpedientePacientes.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {

        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuario.Include(u => u.Medico).Include(u => u.Paciente).ToListAsync();

            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            ViewBag.Medicos = _context.Medico
                .Where(m => !_context.Usuario.Any(u => u.MedicoId == m.Id))
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Id.ToString()
                })
                .ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Usuario usuario, int? MedicoId)
        {
            if (ModelState.IsValid)
            {
                usuario.Bloqueado = false;
                if (usuario.Rol == Roles.Paciente)
                {
                    var paciente = new Paciente();

                    _context.Paciente.Add(paciente);
                    await _context.SaveChangesAsync();

                    usuario.PacienteId = paciente.Id;
                }
                if (usuario.Rol == Roles.Medico)
                {
                    usuario.MedicoId = MedicoId;
                }
                    

                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return View(usuario);

        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Usuario usuario)
        {
            var usuarioToDelete = await _context.Usuario.FindAsync(usuario.Id);
            if (usuarioToDelete == null)
                return NotFound();
            _context.Usuario.Remove(usuarioToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]

        public async Task<IActionResult> Modificar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public async Task<IActionResult> CambiarEstado(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();
            usuario.Bloqueado = !usuario.Bloqueado;
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    } 

}
