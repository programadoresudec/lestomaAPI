using AutoMapper;
using lestoma.Api.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{

    [Route("api/usuarios")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUsuarioService _service;

        public UsersController(IMapper mapper, IDataProtectionProvider protectorProvider, IUsuarioService usuarioService)
            : base(mapper, protectorProvider)
        {
            _service = usuarioService;
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("activos-sin-upa")]
        public async Task<IActionResult> GetUserswithoutUpa()
        {
            var listado = await _service.GetUserswithoutUpa();
            return Ok(listado);
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("listar")]
        public async Task<IActionResult> GetUsers()
        {
            var listado = await _service.GetUsers();
            return Ok(listado);
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("listado")]
        public async Task<IActionResult> GetInfoUsuarios()
        {
            var listado = await _service.GetInfoUsers();
            return Ok(listado);
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador, TipoRol.Auxiliar)]
        [HttpGet("upa-asignada")]
        public async Task<IActionResult> GetUpaDelUsuario()
        {
            int idUser = UserIdDesencrypted();
            var response = await _service.GetUpaUserId(idUser);
            return Ok(response);
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("listado-estados")]
        public async Task<IActionResult> GetEstados()
        {
            var listado = await _service.GetUserStatuses();
            return Ok(listado);
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("listado-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var listado = await _service.GetRoles();
            return Ok(listado);
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        [HttpGet("search/{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var response = await _service.GetByIdAsync(id);
            var usuarioDTOSalida = Mapear<EUsuario, InfoUserDTO>((EUsuario)response.Data);
            response.Data = usuarioDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> RegistrarUsuario(RegistroRequest registro)
        {
            var entidad = Mapear<RegistroRequest, EUsuario>(registro);
            Respuesta = await _service.RegisterUser(entidad, false);
            return Created(string.Empty, Respuesta);
        }

        [HttpPut("editar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> EditarUsuario(RegistroUpdateRequest registro)
        {
            var entidad = Mapear<RegistroUpdateRequest, EUsuario>(registro);
            Respuesta = await _service.UpdateUser(entidad);
            return Ok(Respuesta);
        }


        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpPut("editar-rol")]
        public async Task<IActionResult> EditarRol(RolRequest user)
        {
            var response = await _service.EditRol(user);
            return Ok(response);
        }

    }
}
