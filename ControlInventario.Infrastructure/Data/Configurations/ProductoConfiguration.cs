using ControlInventario.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlInventario.Infrastructure.Data.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> entity)
        {
            entity.ToTable("productos");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CategoriaId)
                .HasColumnName("categoria_id")
                .IsRequired();

            entity.Property(e => e.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.PrecioCompra)
                .HasColumnName("precio_compra")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            entity.Property(e => e.PrecioVenta)
                .HasColumnName("precio_venta")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            entity.Property(e => e.StockActual)
                .HasColumnName("stock_actual")
                .IsRequired()
                .HasDefaultValue(0);

            entity.Property(e => e.Activo)
                .HasColumnName("activo")
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(e => e.CreadoEn)
                .HasColumnName("creado_en")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.ActualizadoEn)
                .HasColumnName("actualizado_en")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(e => e.Categoria)
                .WithMany(e => e.Productos)
                .HasForeignKey(e => e.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}