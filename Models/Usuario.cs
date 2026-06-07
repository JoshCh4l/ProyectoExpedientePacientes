namespace ProyectoExpedientePacientes.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(9)]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Contrasenia { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Administrador|Medico|Paciente)$", ErrorMessage = "El rol debe ser Administrador, Medico o Paciente")]
        public string Rol { get; set; } = string.Empty;

        public bool Bloqueado { get; set; }

        public int? MedicoId { get; set; }
        public Medico? Medico { get; set; }
    }


}
