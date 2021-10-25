using lestoma.Api.Helpers;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lestoma.Api.Controllers
{

    [Route("api/usuarios")]

    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _service = usuarioService;
        }

        [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
        [HttpGet("listado-nombres")]
        public IActionResult GetUsuarios()
        {
            var listado = _service.GetUsersJustNames();
            return Ok(listado);
        }
    }
}
