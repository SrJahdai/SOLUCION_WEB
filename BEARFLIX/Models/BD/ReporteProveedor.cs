using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

[Index("IdReporte", "IdProveedor", "IdTipo", Name = "UQ__ReporteP__97F8BA38BBAF9EFF", IsUnique = true)]
[Index("IdProveedor", "IdTipo", Name = "idx_reporte_proveedor_tipo")]
public partial class ReporteProveedor
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_reporte")]
    public int IdReporte { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [Column("total_ganancias", TypeName = "decimal(10, 2)")]
    public decimal TotalGanancias { get; set; }

    [Column("monto_a_pagar", TypeName = "decimal(10, 2)")]
    public decimal MontoAPagar { get; set; }

    [ForeignKey("IdProveedor")]
    [InverseProperty("ReporteProveedor")]
    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdReporte")]
    [InverseProperty("ReporteProveedor")]
    public virtual Reporte IdReporteNavigation { get; set; } = null!;

    [ForeignKey("IdTipo")]
    [InverseProperty("ReporteProveedor")]
    public virtual TipoVenta IdTipoNavigation { get; set; } = null!;
}
