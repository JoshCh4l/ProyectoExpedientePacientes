using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Data;
using ProyectoExpedientePacientes.Models;

namespace ProyectoExpedientePacientes.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EspecialidadController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EspecialidadController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var especialidades = await _context.Especialidad.ToListAsync();

            return View(especialidades);

        }

        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Especialidad especialidad)
        {

            if (ModelState.IsValid) { 
            
                _context.Especialidad.Add(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(especialidad);

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var especialidad = await _context.Especialidad.FindAsync(id);

            if (especialidad == null) 
                return NotFound();

            return View(especialidad);
        
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Especialidad especialidad)
        {

            var especialidadToDelete = await _context.Especialidad.FindAsync(especialidad.Id);
            
            if (especialidadToDelete == null) 
                return NotFound();

            _context.Especialidad.Remove(especialidadToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        { 
        
            var especialidad = await _context.Especialidad.FindAsync(id);

            if (especialidad == null)
                return NotFound();

            return View(especialidad);

        }

        [HttpPost]
        public async Task<IActionResult> Modificar(Especialidad especialidad)
        {

            if (ModelState.IsValid) 
            {

                _context.Especialidad.Update(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(especialidad);

        }

        }

}