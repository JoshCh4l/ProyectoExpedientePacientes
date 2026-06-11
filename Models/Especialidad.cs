using System.ComponentModel.DataAnnotations;

namespace ProyectoExpedientePacientes.Models
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //Min?
        //Max?
        public string Nombre { get; set; } = string.Empty;

        public ICollection<MedicoEspecialidad> MedicoEspecialidades { get; set; }
        = new List<MedicoEspecialidad>();

    }
}
