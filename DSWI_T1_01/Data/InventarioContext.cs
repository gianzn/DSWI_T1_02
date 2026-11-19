using System;
using System.Collections.Generic;
using DSWI_T1_01.Models;
using Microsoft.EntityFrameworkCore;

namespace DSWI_T1_01.Data;

public partial class InventarioContext : DbContext
{
    public InventarioContext(DbContextOptions<InventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24B7EE1505D7");

            entity.ToTable("Inventario");

            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Ubicacion).HasMaxLength(100);

            entity.HasOne(d => d.Producto).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Inventari__Produ__398D8EEE");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AE83FDEE9F9A");

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Categoria).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
