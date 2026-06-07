using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoExpedientePacientes.Models
{
    public class MedicoEspecialidad
    {
        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null!;
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; } = null!;

    }
}
