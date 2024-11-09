using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.DB;

[Index("Correo", Name = "UQ__Usuario__2A586E0B1F911ACD", IsUnique = true)]
[Index("Correo", Name = "idx_usuario_correo", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("correo")]
    [StringLength(255)]
    [Unicode(false)]
    public string Correo { get; set; } = null!;

    [Column("fecha_nacimiento")]
    public DateOnly FechaNacimiento { get; set; }

    [Column("contrasena")]
    [StringLength(255)]
    [Unicode(false)]
    public string Contrasena { get; set; } = null!;

    [Column("foto")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Foto { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("fecha_baja", TypeName = "datetime")]
    public DateTime? FechaBaja { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Puntaje> Puntaje { get; set; } = new List<Puntaje>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Tarjeta> Tarjeta { get; set; } = new List<Tarjeta>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("IdUsuario")]
    public virtual ICollection<Rol> IdRol { get; set; } = new List<Rol>();
}
