using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

[Index("Descripcion", Name = "UQ__Banco__298336B6A846EEF5", IsUnique = true)]
public partial class Banco
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("descripcion")]
    [StringLength(100)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdBancoNavigation")]
    public virtual ICollection<Tarjeta> Tarjeta { get; set; } = new List<Tarjeta>();
}
