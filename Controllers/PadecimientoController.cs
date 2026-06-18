using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Data;
using ProyectoExpedientePacientes.Models;

namespace ProyectoExpedientePacientes.Controllers
{

    [Authorize(Roles = "Administrador, Medico")]
    public class PadecimientoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PadecimientoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var padecimientos = 
                await _context.Padecimiento.ToListAsync();

            return View(padecimientos);

        }

        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Padecimiento padecimiento)
        {

            if (ModelState.IsValid)
            {

                _context.Padecimiento.Add(padecimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(padecimiento);

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var padecimiento = 
                await _context.Padecimiento.FindAsync(id);

            if (padecimiento == null)
                return NotFound();

            return View(padecimiento);

        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Padecimiento padecimiento)
        {

            var padecimientoToDelete = 
                await _context.Padecimiento.FindAsync(padecimiento.Id);

            if (padecimientoToDelete == null)
                return NotFound();

            _context.Padecimiento.Remove(padecimientoToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {

            var padecimiento = 
                await _context.Padecimiento.FindAsync(id);

            if (padecimiento == null)
                return NotFound();

            return View(padecimiento);

        }

        [HttpPost]
        public async Task<IActionResult> Modificar(Padecimiento padecimiento)
        {

            if (ModelState.IsValid)
            {

                _context.Padecimiento.Update(padecimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(padecimiento);

        }

    }

}