using BEARFLIX.Models;
using BEARFLIX.Models.BD;
using System.Threading.Tasks;

namespace BEARFLIX.Servicios
{
    public interface IAutenticacion
    {
        Task<Usuario> AutenticarUsuario(string correo, string contrasena);
    }
}
