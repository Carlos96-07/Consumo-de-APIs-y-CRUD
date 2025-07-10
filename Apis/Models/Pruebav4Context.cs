using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Apis.Models;

public partial class Pruebav4Context : DbContext
{
    public Pruebav4Context()
    {
    }

    public Pruebav4Context(DbContextOptions<Pruebav4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Detalleventum> Detalleventa { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Detalleventum>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DETALLEV__B4F46A570F742678");

            entity.ToTable("DETALLEVENTA");

            entity.Property(e => e.IdDetalle).HasColumnName("ID_DETALLE");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("CANTIDAD");
            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.Impuesto).HasColumnName("IMPUESTO");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PRECIO_UNITARIO");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SUBTOTAL");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TOTAL");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_PRODUCTO");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__88BD03570FDC55A6");

            entity.ToTable("PRODUCTOS");

            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PRECIO_UNITARIO");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__DE4431C59D1E86C9");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
