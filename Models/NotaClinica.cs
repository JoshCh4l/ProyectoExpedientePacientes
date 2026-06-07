using System.ComponentModel.DataAnnotations;

namespace ProyectoExpedientePacientes.Models
{
    public class NotaClinica
    {
        [Key]
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null!;

        [Required]
        public string Contenido { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

    }
}
