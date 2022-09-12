using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{

    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
            : base(mapper)
        {
            _service = usuarioService;
        }

        [Authorize(Roles = RolesEstaticos.SUPERADMIN + "," + RolesEstaticos.ADMIN)]
        [HttpGet("activos")]
        public IActionResult GetUsuarios()
        {
            var listado = _service.GetUsersJustNames(IsSuperAdmin());
            return Ok(listado);
        }

        [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
        [HttpGet("listado")]
        public async Task<IActionResult> GetInfoUsuarios()
        {
            var listado = await _service.GetInfoUsers();
            return Ok(listado);
        }

        [Authorize(Roles = RolesEstaticos.SUPERADMIN + "," + RolesEstaticos.ADMIN)]
        [HttpGet("search/{id}")]
        public async Task<IActionResult> getUsuario(int id)
        {
            var response = await _service.GetByIdAsync(id);
            var usuarioDTOSalida = Mapear<EUsuario, InfoUserDTO>((EUsuario)response.Data);
            response.Data = usuarioDTOSalida;
            return Ok(response);
        }

        [HttpPost("agregar")]
        [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
        public async Task<IActionResult> RegistrarUsuario(RegistroRequest registro)
        {
            var entidad = Mapear<RegistroRequest, EUsuario>(registro);
            Respuesta = await _service.RegisterUser(entidad, false);
            Respuesta.Data = registro;
            Respuesta.StatusCode = (int)HttpStatusCode.Created;
            return Created(string.Empty, Respuesta);
        }

        [HttpPost("editar")]
        [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
        public async Task<IActionResult> EditarUsuario(RegistroRequest registro)
        {
            var entidad = Mapear<RegistroRequest, EUsuario>(registro);
            Respuesta = await _service.UpdateUser(entidad);
            Respuesta.Data = registro;
            Respuesta.StatusCode = (int)HttpStatusCode.Created;
            return Created(string.Empty, Respuesta);
        }


        [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
        [HttpPut("editar-rol")]
        public async Task<IActionResult> EditarRol(RolRequest user)
        {
            var response = await _service.EditRol(user);
            return Ok(response);
        }

    }
}
