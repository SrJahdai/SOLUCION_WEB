using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

[PrimaryKey("IdUsuario", "IdPelicula")]
[Index("IdUsuario", "Puntaje1", Name = "idx_puntaje_usuario")]
public partial class Puntaje
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Key]
    [Column("id_pelicula")]
    public int IdPelicula { get; set; }

    [Column("puntaje")]
    public byte Puntaje1 { get; set; }

    [ForeignKey("IdPelicula")]
    [InverseProperty("Puntaje")]
    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Puntaje")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
