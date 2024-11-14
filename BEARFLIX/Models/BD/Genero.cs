using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.BD;

[Index("Descripcion", Name = "UQ__Genero__298336B6F1ABFD5C", IsUnique = true)]
public partial class Genero
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("descripcion")]
    [StringLength(20)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [ForeignKey("IdGenero")]
    [InverseProperty("IdGenero")]
    public virtual ICollection<Pelicula> IdPelicula { get; set; } = new List<Pelicula>();
}
