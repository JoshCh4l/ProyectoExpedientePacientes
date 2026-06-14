using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Data;
using ProyectoExpedientePacientes.Models;

namespace ProyectoExpedientePacientes.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class MedicoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MedicoController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string buscar)
        {
            var query = _context.Medico
                .Include(m => m.MedicoEspecialidades)
                .ThenInclude(me => me.Especialidad)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(m =>
                    m.NombreCompleto.Contains(buscar) ||
                    m.NumeroColegiado.Contains(buscar));
            }

            return View(await query.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Especialidades = _context.Especialidad.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Medico medico,
            IFormFile foto,
            List<int> especialidadesSeleccionadas)
        {

            if (foto == null)
            {
                ModelState.AddModelError("", "Debe seleccionar una fotografía.");
            }

            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    using var ms = new MemoryStream();
                    await foto.CopyToAsync(ms);

                    medico.Fotografia = ms.ToArray();
                }

                _context.Medico.Add(medico);
                await _context.SaveChangesAsync();

                foreach (var especialidadId in especialidadesSeleccionadas)
                {
                    _context.MedicoEspecialidad.Add(
                        new MedicoEspecialidad
                        {
                            MedicoId = medico.Id,
                            EspecialidadId = especialidadId
                        });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Especialidades = _context.Especialidad.ToList();
            return View(medico);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var medico = await _context.Medico
                .Include(m => m.MedicoEspecialidades)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medico == null)
                return NotFound();

            ViewBag.Especialidades = _context.Especialidad.ToList();

            return View(medico);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Medico medico,
            IFormFile foto,
            List<int> especialidadesSeleccionadas)
        {
            var medicoDb = await _context.Medico
                .Include(m => m.MedicoEspecialidades)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicoDb == null)
                return NotFound();

            medicoDb.NombreCompleto = medico.NombreCompleto;
            medicoDb.NumeroColegiado = medico.NumeroColegiado;

            if (foto != null)
            {
                using var ms = new MemoryStream();
                await foto.CopyToAsync(ms);

                medicoDb.Fotografia = ms.ToArray();
            }

            _context.MedicoEspecialidad.RemoveRange(
                medicoDb.MedicoEspecialidades);

            foreach (var especialidadId in especialidadesSeleccionadas)
            {
                _context.MedicoEspecialidad.Add(
                    new MedicoEspecialidad
                    {
                        MedicoId = medicoDb.Id,
                        EspecialidadId = especialidadId
                    });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var medico = await _context.Medico
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medico == null)
                return NotFound();

            return View(medico);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medico
                .Include(m => m.MedicoEspecialidades)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medico != null)
            {
                _context.MedicoEspecialidad.RemoveRange(
                    medico.MedicoEspecialidades);

                _context.Medico.Remove(medico);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
