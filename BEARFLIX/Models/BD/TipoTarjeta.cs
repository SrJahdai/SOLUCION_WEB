using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

[Index("Descripcion", Name = "UQ__TipoTarj__298336B67DC1FCCB", IsUnique = true)]
public partial class TipoTarjeta
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("descripcion")]
    [StringLength(20)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdTipoNavigation")]
    public virtual ICollection<Tarjeta> Tarjeta { get; set; } = new List<Tarjeta>();
}
