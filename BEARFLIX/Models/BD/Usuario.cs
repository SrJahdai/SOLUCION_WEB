using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BEARFLIX.Models.BD
{

    [Index("Email", Name = "UQ__Usuario__2A586E0B4FD6CE3F", IsUnique = true)]
    public class Usuario : IdentityUser<int>
    {
        [Column("fecha_nacimiento")]
        public DateOnly FechaNacimiento { get; set; }


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
}
