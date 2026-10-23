using ControlInventario.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlInventario.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entity)
        {
            entity.ToTable("usuarios");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RolId)
                .HasColumnName("rol_id")
                .IsRequired();

            entity.Property(e => e.NombreCompleto)
                .HasColumnName("nombre_completo")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Activo)
                .HasColumnName("activo")
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(e => e.CreadoEn)
                .HasColumnName("creado_en")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(e => e.Rol)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(e => e.RolId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}