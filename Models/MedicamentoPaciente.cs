using System.ComponentModel.DataAnnotations;

namespace ProyectoExpedientePacientes.Models
{
    public class MedicamentoPaciente
    {
        [Key]
        public int Id { get; set; }
        
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; } = null!;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public bool Suspendido { get; set; }
    }
}
