using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.DB;

[Index("Descripcion", Name = "UQ__TipoVent__298336B6C6C8068D", IsUnique = true)]
public partial class TipoVenta
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("descripcion")]
    [StringLength(20)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdTipoNavigation")]
    public virtual ICollection<ReporteProveedor> ReporteProveedor { get; set; } = new List<ReporteProveedor>();

    [InverseProperty("IdTipoNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
