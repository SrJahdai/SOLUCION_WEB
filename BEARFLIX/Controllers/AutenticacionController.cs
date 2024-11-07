using BEARFLIX.Models.BD;
using BEARFLIX.Models.DTO;
using BEARFLIX.Servicios;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AutenticacionController : ControllerBase
{



    private readonly Autenticacion _autenticacion;

    public AutenticacionController(Autenticacion autenticacion)
    {
        _autenticacion = autenticacion;
    }

    // GET: api/autenticacion/usuarios
    [HttpGet("usuarios")]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        var usuarios = await _autenticacion.ObtenerUsuarios();
        return Ok(usuarios);
    }

    // GET: api/autenticacion/usuarios/{id}
    [HttpGet("usuarios/{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _autenticacion.ObtenerUsuarioPorId(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    // POST: api/autenticacion/registro
    [HttpPost("registro")]
    public async Task<ActionResult<Usuario>> Register([FromBody] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var resultado = await _autenticacion.RegistrarUsuario(usuario);
                return CreatedAtAction(nameof(GetUsuario), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        return BadRequest(ModelState);
    }

    // PUT: api/autenticacion/usuarios/{id}
    [HttpPut("usuarios/{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return BadRequest();
        }

        var resultado = await _autenticacion.ActualizarUsuario(usuario);
        if (resultado == null)
        {
            return NotFound();
        }

        return NoContent(); // Se realiza la actualización correctamente
    }

    // DELETE: api/autenticacion/usuarios/{id}
    [HttpDelete("usuarios/{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var resultado = await _autenticacion.EliminarUsuario(id);
        if (!resultado)
        {
            return NotFound();
        }

        return NoContent(); // Usuario eliminado correctamente
    }

    // POST: api/autenticacion/login
    // POST: /autenticacion/login
    [HttpPost("login")]
    public async Task<ActionResult<Usuario>> Login([FromBody] LoginRequerimiento request)
    {
        try
        {
            var usuario = await _autenticacion.AutenticarUsuario(request.Correo, request.Contrasena);
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
