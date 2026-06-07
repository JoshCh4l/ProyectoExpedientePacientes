using Microsoft.EntityFrameworkCore;
using ProyectoExpedientePacientes.Models;

namespace ProyectoExpedientePacientes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Especialidad> Especialidad { get; set; }
        public DbSet<MedicoEspecialidad> MedicoEspecialidad { get; set; }
        public DbSet<Medicamento> Medicamento { get; set; }
        public DbSet<Padecimiento> Padecimiento { get; set; }
        public DbSet<Tratamiento> Tratamiento { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<MedicamentoPaciente> MedicamentoPaciente { get; set; }
        public DbSet<TratamientoPaciente> TratamientoPaciente { get; set; }
        public DbSet<PadecimientoPaciente> PadecimientoPaciente { get; set; }
        public DbSet<NotaClinica> NotaClinica { get; set; }
        public DbSet<ResultadoExamenMedico> ResultadoExamenMedico { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MedicoEspecialidad>()
                .HasKey(me => new
                {
                    me.MedicoId,
                    me.EspecialidadId
                });
        }

    }
}
