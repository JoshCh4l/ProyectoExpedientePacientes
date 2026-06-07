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

        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public bool Suspendido { get; set; }
    }
}
