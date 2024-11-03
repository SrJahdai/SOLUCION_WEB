using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

[Index("Titulo", Name = "UQ__Pelicula__38FA640FCFB2E8C2", IsUnique = true)]
[Index("Titulo", Name = "idx_pelicula_titulo")]
public partial class Pelicula
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("titulo")]
    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(255)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("duracion")]
    public int Duracion { get; set; }

    [Column("portada")]
    [StringLength(255)]
    [Unicode(false)]
    public string Portada { get; set; } = null!;

    [Column("fondo")]
    [StringLength(255)]
    [Unicode(false)]
    public string Fondo { get; set; } = null!;

    [Column("titulo_imagen")]
    [StringLength(255)]
    [Unicode(false)]
    public string TituloImagen { get; set; } = null!;

    [Column("video")]
    [StringLength(255)]
    [Unicode(false)]
    public string Video { get; set; } = null!;

    [Column("precio_compra", TypeName = "decimal(10, 2)")]
    public decimal PrecioCompra { get; set; }

    [Column("precio_renta", TypeName = "decimal(10, 2)")]
    public decimal PrecioRenta { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [ForeignKey("IdProveedor")]
    [InverseProperty("Pelicula")]
    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    [InverseProperty("IdPeliculaNavigation")]
    public virtual ICollection<Puntaje> Puntaje { get; set; } = new List<Puntaje>();

    [InverseProperty("IdPeliculaNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    [ForeignKey("IdPelicula")]
    [InverseProperty("IdPelicula")]
    public virtual ICollection<Genero> IdGenero { get; set; } = new List<Genero>();
}
