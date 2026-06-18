using System.ComponentModel.DataAnnotations;

namespace ProyectoExpedientePacientes.Models
{
    public class TratamientoPaciente
    {
        [Key]
        public int Id { get; set; }
        
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int TratamientoId { get; set; }
        public Tratamiento Tratamiento { get; set; } = null!;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public bool Suspendido { get; set; }
    }
}
