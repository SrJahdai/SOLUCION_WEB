using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.DB;

[Index("Descripcion", Name = "UQ__Rol__298336B600AECE91", IsUnique = true)]
public partial class Rol
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("descripcion")]
    [StringLength(30)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [ForeignKey("IdRol")]
    [InverseProperty("IdRol")]
    public virtual ICollection<Permiso> IdPermiso { get; set; } = new List<Permiso>();

    [ForeignKey("IdRol")]
    [InverseProperty("IdRol")]
    public virtual ICollection<Usuario> IdUsuario { get; set; } = new List<Usuario>();
}
