namespace ProyectoExpedientePacientes.Models
{
    public class ModeloVistaPaciente
    {
        
        public int PacienteId { get; set; }
        public string Nombre { get; set; } = "";
        public string Cedula { get; set; } = "";
        public string Correo { get; set; } = "";

        public DateTime? UltimaAtencion { get; set; }
        
    }
}
