namespace Universidad.Data
{
    using Microsoft.EntityFrameworkCore;
    using Universidad.Models;

    public class UniversidadContext : DbContext
    {
        public UniversidadContext(DbContextOptions<UniversidadContext> options)
            : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(a => a.Id); 
                entity.Property(a => a.Nombre)
                      .IsRequired()
                      .HasMaxLength(100); 

                entity.Property(a => a.Matricula)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(a => a.Correo)
                      .HasMaxLength(100);

                entity.Property(a => a.FechaNacimiento)
                      .IsRequired();

                entity.Property(a => a.Carrera)
                      .HasMaxLength(50);

                entity.Property(a => a.Promedio)
                      .HasColumnType("decimal(4, 2)");
            });
        }
    }
}

