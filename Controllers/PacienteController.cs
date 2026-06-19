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
                .Include(n => n.Medico)
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
            var medico = int.Parse(User.FindFirst("MedicoId")!.Value);

            var nota = new NotaClinica
            {
                PacienteId = id,
                MedicoId = medico,
                Contenido = contenido,
                FechaRegistro = DateTime.Now
            };

            _context.NotaClinica.Add(nota);
            await _context.SaveChangesAsync();
            return RedirectToAction("Historial", new { id });

        }


        [HttpPost]
        public async Task<IActionResult> AgregarPadecimiento(int pacienteId, int padecimientoId)
        {


            int usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

            var registro = new PadecimientoPaciente
            {
                PacienteId = pacienteId,
                PadecimientoId = padecimientoId,
                UsuarioId = usuarioId,
                FechaRegistro = DateTime.Now,
                Suspendido = false
            };

            _context.PadecimientoPaciente.Add(registro);
            await _context.SaveChangesAsync();

            return RedirectToAction("Expediente", new { id = pacienteId });
        }



        [HttpPost]
        public async Task<IActionResult> AgregarMedicamento(int pacienteId, int medicamentoId)
        {

            int usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

            var registro = new MedicamentoPaciente
            {
                PacienteId = pacienteId,
                MedicamentoId = medicamentoId,
                UsuarioId = usuarioId,
                FechaRegistro = DateTime.Now,
                Suspendido = false
            };

            _context.MedicamentoPaciente.Add(registro);

            await _context.SaveChangesAsync();

            return RedirectToAction("Expediente", new { id = pacienteId });
        }

        [HttpPost]
        public async Task<IActionResult> AgregarTratamiento(int pacienteId, int tratamientoId)
        {
            int usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

            var registro = new TratamientoPaciente
            {
                PacienteId = pacienteId,
                TratamientoId = tratamientoId,
                UsuarioId = usuarioId,
                FechaRegistro = DateTime.Now,
                Suspendido = false
            };

            _context.TratamientoPaciente.Add(registro);

            await _context.SaveChangesAsync();

            return RedirectToAction("Expediente", new { id = pacienteId });
        }

        public async Task<IActionResult> SuspenderPadecimiento(int id)
        {
            var registro = await _context.PadecimientoPaciente.FindAsync(id);

            if (registro != null)
            {
                registro.Suspendido = true;
                await _context.SaveChangesAsync();

                return RedirectToAction("Expediente", new { id = registro.PacienteId });
            }

            return NotFound();
        }


        public async Task<IActionResult> SuspenderMedicamento(int id)
        {
            var registro = await _context.MedicamentoPaciente.FindAsync(id);

            if (registro == null)
                return NotFound();

            registro.Suspendido = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Expediente", new { id = registro.PacienteId });
        }


        public async Task<IActionResult> SuspenderTratamiento(int id)
        {
            var registro = await _context.TratamientoPaciente.FindAsync(id);

            if (registro == null)
                return NotFound();

            registro.Suspendido = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Expediente", new { id = registro.PacienteId });
        }

        public async Task<IActionResult> Expediente(int id)
        {
            ViewBag.PacienteId = id;

            //Asignados al paciente
            ViewBag.PadecimientosPaciente = await _context.PadecimientoPaciente
                .Where(p => p.PacienteId == id)
                .Include(p => p.Padecimiento)
                .Include(p => p.Usuario)
                .ToListAsync();

            ViewBag.MedicamentosPaciente = await _context.MedicamentoPaciente
                .Where(m => m.PacienteId == id)
                .Include(m => m.Medicamento)
                .Include(m => m.Usuario)
                .ToListAsync();

            ViewBag.TratamientosPaciente = await _context.TratamientoPaciente
                .Where(t => t.PacienteId == id)
                .Include(t => t.Tratamiento)
                .Include(t => t.Usuario)
                .ToListAsync();

            //Catálogos disponibles
            ViewBag.Padecimientos = await _context.Padecimiento.ToListAsync();
            ViewBag.Medicamentos = await _context.Medicamento.ToListAsync();
            ViewBag.Tratamientos = await _context.Tratamiento.ToListAsync();


            ViewBag.Examenes = await _context.ResultadoExamenMedico
                .Where(e => e.PacienteId == id)
                .Include(e => e.Usuario)
                .OrderByDescending(e => e.FechaRegistro)
                .ToListAsync();

            return View();
        }


        [HttpGet]
        public IActionResult AgregarExamen(int id)
        {
            ViewBag.PacienteId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarExamen(int pacienteId, string descripcion, IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                ViewBag.Error = "Debe seleccionar un archivo.";
                ViewBag.PacienteId = pacienteId;
                return View();
            }

            if (archivo.ContentType != "application/pdf" &&
                !archivo.ContentType.StartsWith("image/"))
            {
                ViewBag.Error = "Solo se permiten archivos PDF o imágenes.";
                ViewBag.PacienteId = pacienteId;
                return View();
            }

            int usuarioId = int.Parse(User.FindFirst("UsuarioId")!.Value);

            using var memoryStream = new MemoryStream();
            await archivo.CopyToAsync(memoryStream);

            var examen = new ResultadoExamenMedico
            {
                PacienteId = pacienteId,
                UsuarioId = usuarioId,
                Descripcion = descripcion,
                Archivo = memoryStream.ToArray(),
                TipoArchivo = archivo.ContentType,
                FechaRegistro = DateTime.Now
            };

            _context.ResultadoExamenMedico.Add(examen);
            await _context.SaveChangesAsync();

            return RedirectToAction("Expediente", new { id = pacienteId });
        }

        public async Task<IActionResult> VerExamen(int id)
        {
            var examen = await _context.ResultadoExamenMedico.FindAsync(id);

            if (examen == null)
                return NotFound();

            return File(examen.Archivo, examen.TipoArchivo);
        }
    }
}
