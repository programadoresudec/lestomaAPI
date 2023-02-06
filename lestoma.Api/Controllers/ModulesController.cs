using AutoMapper;
using lestoma.Api.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/modulos")]
    [ApiController]
    public class ModulesController : BaseController
    {
        private readonly IModuloService _moduloService;
        public ModulesController(IMapper mapper, IModuloService moduloService)
            : base(mapper)
        {
            _moduloService = moduloService;
        }
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("paginar")]
        public async Task<IActionResult> GetModulosPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _moduloService.GetAllForPagination();
            var listado = await GetPaginacion<EModuloComponente, ModuloDTO>(paginacion, queryable);
            var paginador = Paginador<ModuloDTO>.CrearPaginador(queryable.Count(), listado, paginacion);
            return Ok(paginador);
        }
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("listado")]
        public async Task<IActionResult> GetModulos()
        {
            var query = await _moduloService.GetAll();
            var modulos = Mapear<IEnumerable<EModuloComponente>, IEnumerable<ModuloDTO>>(query);
            return Ok(modulos);
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Auxiliar, TipoRol.Administrador)]
        [HttpGet("listar-nombres")]
        public async Task<IActionResult> GetNombresModulos()
        {
            var query = await _moduloService.GetModulosJustNames();
            return Ok(query);
        }
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModulo(Guid id)
        {
            var response = await _moduloService.GetById(id);
            var moduloDTOSalida = Mapear<EModuloComponente, ModuloDTO>((EModuloComponente)response.Data);
            response.Data = moduloDTOSalida;
            return Ok(response);
        }
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpPost("crear")]
        public async Task<IActionResult> CrearModulo(ModuloRequest modulo)
        {
            var moduloDTO = Mapear<ModuloRequest, EModuloComponente>(modulo);
            var response = await _moduloService.Create(moduloDTO);
            var upaDTOSalida = Mapear<EModuloComponente, ModuloRequest>((EModuloComponente)response.Data);
            response.Data = upaDTOSalida;
            return CreatedAtAction(nameof(GetModulo), new { id = ((ModuloRequest)response.Data).Id }, response);
        }
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpPut("editar")]
        public async Task<IActionResult> EditarModulo(ModuloRequest modulo)
        {
            var moduloDTO = Mapear<ModuloRequest, EModuloComponente>(modulo);
            var response = await _moduloService.Update(moduloDTO);
            var moduloDTOSalida = Mapear<EModuloComponente, ModuloRequest>((EModuloComponente)response.Data);
            response.Data = moduloDTOSalida;
            return Ok(response);
        }
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarModulo(Guid id)
        {
            await _moduloService.Delete(id);
            return NoContent();
        }
    }
}
