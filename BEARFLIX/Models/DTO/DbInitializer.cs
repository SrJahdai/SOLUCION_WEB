using BEARFLIX.Models.BD;

namespace BEARFLIX.Models.DTO
{
    public static class DbInitializer
    {
        public static void Initialize(BearflixContext context)
        {

            // Verificar si los roles ya están insertados
            if (context.Rol.Any()) return; // Si ya existen roles, no hacer nada

            // Insertar roles predeterminados
            context.Rol.AddRange(
                new Rol { Descripcion = "USUARIO" },
                new Rol { Descripcion = "DUENO" },
                new Rol { Descripcion = "ADMINISTRADOR" },
                new Rol { Descripcion = "TESTER" }
            );

            if (context.TipoVenta.Any()) return;
            // Insertar tipos de venta predeterminados
            context.TipoVenta.AddRange(
                new TipoVenta { Descripcion = "compra" },
                new TipoVenta { Descripcion = "renta" }
            );

            // Guardar los cambios en la base de datos
            context.SaveChanges();
        }
    }

}
