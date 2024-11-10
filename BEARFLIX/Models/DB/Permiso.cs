﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.DB;

[Index("Descripcion", Name = "UQ__Permiso__298336B66B7BA88C", IsUnique = true)]
public partial class Permiso
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("descripcion")]
    [StringLength(20)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [ForeignKey("IdPermiso")]
    [InverseProperty("IdPermiso")]
    public virtual ICollection<Rol> IdRol { get; set; } = new List<Rol>();
}