    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
namespace BEARFLIX.Filters
{


    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedObjectResult(new { mensaje = "No estás autenticado." });
                return;
            }

            // Obtener el rol único del usuario desde los claims
            var userRole = user.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            // Verificar si el rol del usuario está permitido
            if (userRole == null || !_roles.Contains(userRole))
            {
                context.Result = new JsonResult(new { mensaje = "No tienes permiso para acceder a este recurso." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }

}
