using System.ComponentModel.DataAnnotations;

namespace ProyectoExpedientePacientes.Models
{
    public class ResultadoExamenMedico
    {
        [Key]
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        public byte[] Archivo { get; set; } = null!;

        [Required]
        public string TipoArchivo { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }
    }
}
