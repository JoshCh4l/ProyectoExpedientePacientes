using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Data;
using ProyectoExpedientePacientes.Models;

namespace ProyectoExpedientePacientes.Controllers
{

    [Authorize(Roles = "Administrador, Medico")]
    public class MedicamentoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MedicamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var medicamentos =
                await _context.Medicamento.ToListAsync();

            return View(medicamentos);

        }

        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Medicamento medicamento)
        {

            if (ModelState.IsValid)
            {

                _context.Medicamento.Add(medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(medicamento);

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var medicamento = 
                await _context.Medicamento.FindAsync(id);

            if (medicamento == null)
                return NotFound();

            return View(medicamento);

        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Medicamento medicamento)
        {

            var medicamentoToDelete =
                await _context.Medicamento.FindAsync(medicamento.Id);

            if (medicamentoToDelete == null)
                return NotFound();

            _context.Medicamento.Remove(medicamentoToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {

            var medicamento = 
                await _context.Medicamento.FindAsync(id);

            if (medicamento == null)
                return NotFound();

            return View(medicamento);

        }

        [HttpPost]
        public async Task<IActionResult> Modificar(Medicamento medicamento)
        {

            if (ModelState.IsValid)
            {

                _context.Medicamento.Update(medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(medicamento);

        }

    }

}