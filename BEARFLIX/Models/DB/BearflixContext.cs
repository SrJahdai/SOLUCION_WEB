using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.DB;

public partial class BearflixContext : DbContext
{
    public BearflixContext()
    {
    }

    public BearflixContext(DbContextOptions<BearflixContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banco> Banco { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Pelicula> Pelicula { get; set; }

    public virtual DbSet<Permiso> Permiso { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Puntaje> Puntaje { get; set; }

    public virtual DbSet<Reporte> Reporte { get; set; }

    public virtual DbSet<ReporteProveedor> ReporteProveedor { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Tarjeta> Tarjeta { get; set; }

    public virtual DbSet<TipoTarjeta> TipoTarjeta { get; set; }

    public virtual DbSet<TipoVenta> TipoVenta { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Banco__3213E83FA5571CBC");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genero__3213E83F81BF4720");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pelicula__3213E83FC5BFF2BE");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Pelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pelicula__id_pro__5441852A");

            entity.HasMany(d => d.IdGenero).WithMany(p => p.IdPelicula)
                .UsingEntity<Dictionary<string, object>>(
                    "PeliculaGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("IdGenero")
                        .HasConstraintName("FK__PeliculaG__id_ge__6B24EA82"),
                    l => l.HasOne<Pelicula>().WithMany()
                        .HasForeignKey("IdPelicula")
                        .HasConstraintName("FK__PeliculaG__id_pe__6A30C649"),
                    j =>
                    {
                        j.HasKey("IdPelicula", "IdGenero").HasName("PK__Pelicula__3C9BF102EC442E82");
                        j.HasIndex(new[] { "IdGenero" }, "idx_pelicula_genero");
                        j.IndexerProperty<int>("IdPelicula").HasColumnName("id_pelicula");
                        j.IndexerProperty<int>("IdGenero").HasColumnName("id_genero");
                    });
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permiso__3213E83F27B58F7C");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proveedo__3213E83F6D534381");
        });

        modelBuilder.Entity<Puntaje>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdPelicula }).HasName("PK__Puntaje__856E13595E49EB46");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Puntaje).HasConstraintName("FK__Puntaje__id_peli__6FE99F9F");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Puntaje).HasConstraintName("FK__Puntaje__id_usua__6EF57B66");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reporte__3213E83FF7DCCF28");

            entity.Property(e => e.FechaGeneracion).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<ReporteProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReporteP__3213E83F566369D4");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ReporteProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReportePr__id_pr__778AC167");

            entity.HasOne(d => d.IdReporteNavigation).WithMany(p => p.ReporteProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReportePr__id_re__76969D2E");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.ReporteProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReportePr__id_ti__787EE5A0");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3213E83F5800ABCC");

            entity.HasMany(d => d.IdPermiso).WithMany(p => p.IdRol)
                .UsingEntity<Dictionary<string, object>>(
                    "PermisoRol",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .HasConstraintName("FK__PermisoRo__id_pe__6754599E"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__PermisoRo__id_ro__66603565"),
                    j =>
                    {
                        j.HasKey("IdRol", "IdPermiso").HasName("PK__PermisoR__889447C4CC326B4D");
                        j.IndexerProperty<int>("IdRol").HasColumnName("id_rol");
                        j.IndexerProperty<int>("IdPermiso").HasColumnName("id_permiso");
                    });
        });

        modelBuilder.Entity<Tarjeta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tarjeta__3213E83F925A740A");

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.Tarjeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarjeta__id_banc__5DCAEF64");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Tarjeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarjeta__id_tipo__5EBF139D");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Tarjeta).HasConstraintName("FK__Tarjeta__id_usua__5FB337D6");
        });

        modelBuilder.Entity<TipoTarjeta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoTarj__3213E83FA6262F83");
        });

        modelBuilder.Entity<TipoVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoVent__3213E83FCF71F04A");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F72829DD7");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            entity.HasMany(d => d.IdRol).WithMany(p => p.IdUsuario)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioRol",
                    r => r.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__UsuarioRo__id_ro__6383C8BA"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__UsuarioRo__id_us__628FA481"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdRol").HasName("PK__UsuarioR__5895CFF32AE2DCD7");
                        j.IndexerProperty<int>("IdUsuario").HasColumnName("id_usuario");
                        j.IndexerProperty<int>("IdRol").HasColumnName("id_rol");
                    });
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Venta__3213E83F936AECAF");

            entity.Property(e => e.FechaVenta).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Venta).HasConstraintName("FK__Venta__id_pelicu__59063A47");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venta__id_tipo__59FA5E80");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta).HasConstraintName("FK__Venta__id_usuari__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
