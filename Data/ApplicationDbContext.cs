using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaTelefonico.Models;

namespace SistemaTelefonico.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Telefono> Telefonos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.ToTable("Telefonos", t => t.ExcludeFromMigrations());

            entity.HasKey(e => e.Id);

            entity.Property(e => e.NombreDueno)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200);

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.HasIndex(e => e.NumeroTelefono)
                .IsUnique();
        });
    }
}