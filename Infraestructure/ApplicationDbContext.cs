using Domain;
using Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infraestructure
{
    public partial class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Ubicacion> Ubicacion { get; set; }
        public DbSet<Disponibilidad> Disponibilidad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehiculo>()
            .Property(e => e.Id)
        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Ubicacion>()
            .Property(e => e.Id)
        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Disponibilidad>()
            .Property(e => e.Id)
        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Disponibilidad>()
                .HasOne(p => p.Vehiculo)
                .WithMany(m => m.Disponibilidad)
                .HasForeignKey(p => p.IdVehiculo);

            modelBuilder.Entity<Disponibilidad>()
                .HasOne(p => p.Ubicacion)
                .WithMany(m => m.Disponibilidad)
                .HasForeignKey(p => p.IdUbicacion);

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculo")
                    .HasKey(e => new { e.Id });

                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.Marca).HasColumnName("Marca").IsRequired();
                entity.Property(e => e.Modelo).HasColumnName("Interes").IsRequired();
                entity.Property(e => e.Ano).HasColumnName("Ano").IsRequired();
                entity.Property(e => e.Tipo).HasColumnName("Tipo").IsRequired();
                entity.Property(e => e.Precio).HasColumnName("Precio").IsRequired();
                entity.Property(e => e.Disponible).HasColumnName("Disponible").IsRequired();
                entity.Property(e => e.Localizacion).HasColumnName("Localizacion").IsRequired();
            });

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.ToTable("Ubicacion")
                    .HasKey(e => new { e.Id });

                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.Localidad).HasColumnName("Localidad").IsRequired();
            });

            modelBuilder.Entity<Disponibilidad>(entity =>
            {
                entity.ToTable("Disponibilidad")
                    .HasKey(e => new { e.Id });

                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.IdUbicacion).HasColumnName("IdUbicacion").IsRequired();
                entity.Property(e => e.IdVehiculo).HasColumnName("IdVehiculo").IsRequired();
            });

        }
    }
}
