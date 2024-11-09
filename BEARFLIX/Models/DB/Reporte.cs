using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.DB;

public partial class Reporte
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("rango_inicio")]
    public DateOnly RangoInicio { get; set; }

    [Column("rango_fin")]
    public DateOnly RangoFin { get; set; }

    [Column("fecha_generacion", TypeName = "datetime")]
    public DateTime FechaGeneracion { get; set; }

    [Column("ganancias_totales", TypeName = "decimal(10, 2)")]
    public decimal GananciasTotales { get; set; }

    [Column("ganancias_brutas", TypeName = "decimal(10, 2)")]
    public decimal GananciasBrutas { get; set; }

    [Column("descripcion")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdReporteNavigation")]
    public virtual ICollection<ReporteProveedor> ReporteProveedor { get; set; } = new List<ReporteProveedor>();
}
