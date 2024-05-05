using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PIA_WheelDeal.Models.dbModels;

public partial class BaseDeGatosContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public BaseDeGatosContext()
    {
    }

    public BaseDeGatosContext(DbContextOptions<BaseDeGatosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ImgVehiculo> ImgVehiculos { get; set; }

    public virtual DbSet<PeticionCompra> PeticionCompras { get; set; }

    public virtual DbSet<StatusCatalogo> StatusCatalogos { get; set; }

    public virtual DbSet<TiposCatalogo> TiposCatalogos { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ImgVehiculo>(entity =>
        {
            entity.Property(e => e.IdImg).ValueGeneratedNever();

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.ImgVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImgVehiculos_Vehiculos");
        });

        modelBuilder.Entity<PeticionCompra>(entity =>
        {
            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.PeticionCompraIdEmpleadoNavigations).HasConstraintName("FK_PeticionCompra_Individuos3");

            entity.HasOne(d => d.IdIndNavigation).WithMany(p => p.PeticionCompraIdIndNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeticionCompra_Individuos2");

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.PeticionCompras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeticionCompra_Producto");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.PeticionCompras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeticionCompra_StatusCatalogo");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.IdProd).HasName("PK_Producto");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Vehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehiculos_TiposCatalogo");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdIngreso).HasName("PK_Finanzas");

            entity.HasOne(d => d.IdIndNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Individuos1");

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Vehiculos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
