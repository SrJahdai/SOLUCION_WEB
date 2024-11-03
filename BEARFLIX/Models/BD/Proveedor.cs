using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

[Index("Descripcion", Name = "UQ__Proveedo__298336B608C3003C", IsUnique = true)]
[Index("Descripcion", Name = "idx_proveedor_descripcion")]
public partial class Proveedor
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("porcentaje")]
    public byte Porcentaje { get; set; }

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<Pelicula> Pelicula { get; set; } = new List<Pelicula>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<ReporteProveedor> ReporteProveedor { get; set; } = new List<ReporteProveedor>();
}
