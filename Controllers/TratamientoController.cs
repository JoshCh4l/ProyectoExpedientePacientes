using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Data;
using ProyectoExpedientePacientes.Models;

namespace ProyectoExpedientePacientes.Controllers
{

    [Authorize(Roles = "Administrador, Medico")]
    public class TratamientoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TratamientoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var tratamientos = 
                await _context.Tratamiento.ToListAsync();

            return View(tratamientos);

        }

        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Tratamiento tratamiento)
        {

            if (ModelState.IsValid)
            {

                _context.Tratamiento.Add(tratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(tratamiento);

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var tratamiento = 
                await _context.Tratamiento.FindAsync(id);

            if (tratamiento == null)
                return NotFound();

            return View(tratamiento);

        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Tratamiento tratamiento)
        {

            var tratamientoToDelete = 
                await _context.Tratamiento.FindAsync(tratamiento.Id);

            if (tratamientoToDelete == null)
                return NotFound();

            _context.Tratamiento.Remove(tratamientoToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {

            var tratamiento = 
                await _context.Tratamiento.FindAsync(id);

            if (tratamiento == null)
                return NotFound();

            return View(tratamiento);

        }

        [HttpPost]
        public async Task<IActionResult> Modificar(Tratamiento tratamiento)
        {

            if (ModelState.IsValid)
            {

                _context.Tratamiento.Update(tratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(tratamiento);

        }

    }

}