using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

[Index("FechaVenta", Name = "idx_venta_fecha")]
[Index("IdPelicula", Name = "idx_venta_pelicula")]
[Index("IdTipo", Name = "idx_venta_tipo")]
[Index("IdUsuario", Name = "idx_venta_usuario")]
public partial class Venta
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_pelicula")]
    public int IdPelicula { get; set; }

    [Column("fecha_venta", TypeName = "datetime")]
    public DateTime FechaVenta { get; set; }

    [Column("expiracion", TypeName = "datetime")]
    public DateTime? Expiracion { get; set; }

    [Column("monto", TypeName = "decimal(10, 2)")]
    public decimal Monto { get; set; }

    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [ForeignKey("IdPelicula")]
    [InverseProperty("Venta")]
    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    [ForeignKey("IdTipo")]
    [InverseProperty("Venta")]
    public virtual TipoVenta IdTipoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Venta")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
