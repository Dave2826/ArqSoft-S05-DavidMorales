using Microsoft.EntityFrameworkCore;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Data
{
    public class CitasAppDbContext : DbContext
    {
        public CitasAppDbContext(DbContextOptions<CitasAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Medico> Medicos => Set<Medico>();
        public DbSet<Cita> Citas => Set<Cita>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("Pacientes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Apellido).IsRequired();
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("Medicos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Apellido).IsRequired();
                entity.Property(e => e.Especialidad).IsRequired();
            });

            modelBuilder.Entity<Cita>(entity =>
            {
                entity.ToTable("Citas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Motivo).IsRequired();
                entity.Property(e => e.Estado).IsRequired();

                entity.HasOne<Paciente>()
                    .WithMany()
                    .HasForeignKey(c => c.PacienteId);

                entity.HasOne<Medico>()
                    .WithMany()
                    .HasForeignKey(c => c.MedicoId);
            });
        }
    }
}
