using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Models.DB;

public partial class Tarjeta
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("numero")]
    [StringLength(20)]
    [Unicode(false)]
    public string Numero { get; set; } = null!;

    [Column("titular")]
    [StringLength(100)]
    [Unicode(false)]
    public string Titular { get; set; } = null!;

    [Column("id_banco")]
    public int IdBanco { get; set; }

    [Column("vencimiento")]
    public DateOnly Vencimiento { get; set; }

    [Column("ultimos_cuatro")]
    [StringLength(4)]
    [Unicode(false)]
    public string UltimosCuatro { get; set; } = null!;

    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("IdBanco")]
    [InverseProperty("Tarjeta")]
    public virtual Banco IdBancoNavigation { get; set; } = null!;

    [ForeignKey("IdTipo")]
    [InverseProperty("Tarjeta")]
    public virtual TipoTarjeta IdTipoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Tarjeta")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
