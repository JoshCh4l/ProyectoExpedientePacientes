using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Data;
using System.Security.Claims;

namespace ProyectoExpedientePacientes.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExpedienteController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ExpedienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("padecimientos")]
        public IActionResult ObtenerPadecimientos()
        {
            var pacienteId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var padecimientos = _context.PadecimientoPaciente
                .Where(pp => pp.PacienteId == pacienteId)
                .Include(pp => pp.Padecimiento)
                .Select(pp => new
                {
                    pp.Padecimiento.Id,
                    pp.Padecimiento.Nombre,
                    pp.FechaRegistro,
                    pp.Suspendido
                })
                .ToList();

            return Ok(padecimientos);
        }

        [HttpGet("tratamientos")]
        public IActionResult ObtenerTratamientos()
        {
            var pacienteId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var tratamientos = _context.TratamientoPaciente
                .Where(tp => tp.PacienteId == pacienteId)
                .Include(tp => tp.Tratamiento)
                .Select(tp => new
                {
                    tp.Tratamiento.Id,
                    tp.Tratamiento.Nombre,
                    tp.FechaRegistro,
                    tp.Suspendido
                })
                .ToList();

            return Ok(tratamientos);
        }

        [HttpGet("medicamentos")]
        public IActionResult ObtenerMedicamentos()
        {
            var pacienteId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var medicamentos = _context.MedicamentoPaciente
                .Where(mp => mp.PacienteId == pacienteId)
                .Include(mp => mp.Medicamento)
                .Select(mp => new
                {
                    mp.Medicamento.Id,
                    mp.Medicamento.Nombre,
                    mp.FechaRegistro,
                    mp.Suspendido
                })
                .ToList();

            return Ok(medicamentos);
        }

        [HttpGet("examenes")]
        public IActionResult ObtenerExamenesLaboratorio()
        {
            var pacienteId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var examenes = _context.ResultadoExamenMedico
                .Where(e => e.PacienteId == pacienteId)
                .Include(e => e.Usuario)
                .Select(e => new
                {
                    e.Usuario.Nombre,
                    e.Descripcion,
                    e.Archivo,
                    e.TipoArchivo,
                    e.FechaRegistro
                })
                .ToList();

            return Ok(examenes);
        }

        [HttpGet("historial")]
        public IActionResult ObtenerHistorialClinico()
        {
            var pacienteId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var historial = _context.NotaClinica
                .Where(h => h.PacienteId == pacienteId)
                .Include(h => h.Usuario)
                .Select(h => new
                {
                    h.Usuario.Nombre,
                    h.Contenido,
                    h.FechaRegistro
                })
                .ToList();

            return Ok(historial);
        }

    }
}
