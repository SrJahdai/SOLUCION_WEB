using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

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
            entity.HasKey(e => e.Id).HasName("PK__Banco__3213E83F453A5249");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genero__3213E83F45240470");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pelicula__3213E83FBFB2EB9D");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Pelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pelicula__id_pro__66603565");

            entity.HasMany(d => d.IdGenero).WithMany(p => p.IdPelicula)
                .UsingEntity<Dictionary<string, object>>(
                    "PeliculaGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("IdGenero")
                        .HasConstraintName("FK__PeliculaG__id_ge__7D439ABD"),
                    l => l.HasOne<Pelicula>().WithMany()
                        .HasForeignKey("IdPelicula")
                        .HasConstraintName("FK__PeliculaG__id_pe__7C4F7684"),
                    j =>
                    {
                        j.HasKey("IdPelicula", "IdGenero").HasName("PK__Pelicula__3C9BF10231928B9A");
                        j.HasIndex(new[] { "IdGenero" }, "idx_pelicula_genero");
                        j.IndexerProperty<int>("IdPelicula").HasColumnName("id_pelicula");
                        j.IndexerProperty<int>("IdGenero").HasColumnName("id_genero");
                    });
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permiso__3213E83F17871AFC");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proveedo__3213E83FB22503CD");
        });

        modelBuilder.Entity<Puntaje>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdPelicula }).HasName("PK__Puntaje__856E135952663870");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Puntaje).HasConstraintName("FK__Puntaje__id_peli__02084FDA");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Puntaje).HasConstraintName("FK__Puntaje__id_usua__01142BA1");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reporte__3213E83F24E5CBEB");

            entity.Property(e => e.FechaGeneracion).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<ReporteProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReporteP__3213E83FFF906C72");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ReporteProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReportePr__id_pr__09A971A2");

            entity.HasOne(d => d.IdReporteNavigation).WithMany(p => p.ReporteProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReportePr__id_re__08B54D69");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.ReporteProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReportePr__id_ti__0A9D95DB");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3213E83FF5DAB9A0");

            entity.HasMany(d => d.IdPermiso).WithMany(p => p.IdRol)
                .UsingEntity<Dictionary<string, object>>(
                    "PermisoRol",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .HasConstraintName("FK__PermisoRo__id_pe__797309D9"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__PermisoRo__id_ro__787EE5A0"),
                    j =>
                    {
                        j.HasKey("IdRol", "IdPermiso").HasName("PK__PermisoR__889447C4CE063B3D");
                        j.IndexerProperty<int>("IdRol").HasColumnName("id_rol");
                        j.IndexerProperty<int>("IdPermiso").HasColumnName("id_permiso");
                    });
        });

        modelBuilder.Entity<Tarjeta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tarjeta__3213E83FA1E6AB09");

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.Tarjeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarjeta__id_banc__6FE99F9F");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Tarjeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarjeta__id_tipo__70DDC3D8");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Tarjeta).HasConstraintName("FK__Tarjeta__id_usua__71D1E811");
        });

        modelBuilder.Entity<TipoTarjeta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoTarj__3213E83FA6CA5840");
        });

        modelBuilder.Entity<TipoVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoVent__3213E83F0CA5A2F4");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F1BCC89CA");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            entity.HasMany(d => d.IdRol).WithMany(p => p.IdUsuario)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioRol",
                    r => r.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__UsuarioRo__id_ro__75A278F5"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__UsuarioRo__id_us__74AE54BC"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdRol").HasName("PK__UsuarioR__5895CFF39031F745");
                        j.IndexerProperty<int>("IdUsuario").HasColumnName("id_usuario");
                        j.IndexerProperty<int>("IdRol").HasColumnName("id_rol");
                    });
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Venta__3213E83FD2C93F27");

            entity.Property(e => e.FechaVenta).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Venta).HasConstraintName("FK__Venta__id_pelicu__6B24EA82");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venta__id_tipo__6C190EBB");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta).HasConstraintName("FK__Venta__id_usuari__6A30C649");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
