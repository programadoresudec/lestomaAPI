﻿using AutoMapper;
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
    [AllowAnonymous]
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper) : base(mapper)
        {
            _service = usuarioService;
        }

        //[Authorize(Roles = RolesEstaticos.SUPERADMIN + "," + RolesEstaticos.ADMIN)]
        [HttpGet("listado-nombres")]
        public IActionResult GetUsuarios()
        {
            var listado = _service.GetUsersJustNames();
            return Ok(listado);
        }

        [HttpGet("search/{id}")]
        public async Task<IActionResult> getUsuario(int id)
        {
            var response = await _service.GetByIdAsync(id);
            var usuarioDTOSalida = Mapear<EUsuario, InfoUserDTO>((EUsuario)response.Data);
            response.Data = usuarioDTOSalida;
            return Ok(response);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrarse(RegistroRequest registro)
        {
            var entidad = Mapear<RegistroRequest, EUsuario>(registro);
            Respuesta = await _service.Register(entidad);
            Respuesta.Data = registro;
            Respuesta.StatusCode = (int)HttpStatusCode.Created;
            return Created(string.Empty, Respuesta);
        }
        [HttpPut("editar-rol")]
        public async Task<IActionResult> EditarRol(RolRequest user)
        {
            var response = await _service.EditRol(user);
            return Ok(response);
        }

    }
}
