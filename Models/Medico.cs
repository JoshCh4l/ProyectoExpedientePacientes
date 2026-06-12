using System.ComponentModel.DataAnnotations;

namespace ProyectoExpedientePacientes.Models
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        [MaxLength(9)]
        public string NumeroColegiado { get; set; } = string.Empty;

        //[Required]
        public byte[]? Fotografia { get; set; }

        public ICollection<MedicoEspecialidad> MedicoEspecialidades { get; set; }
        = new List<MedicoEspecialidad>();
    }   
}
