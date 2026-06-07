using System.ComponentModel.DataAnnotations;

namespace ProyectoExpedientePacientes.Models
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public ICollection<MedicoEspecialidad> MedicoEspecialidades { get; set; }
        = new List<MedicoEspecialidad>();

    }
}
