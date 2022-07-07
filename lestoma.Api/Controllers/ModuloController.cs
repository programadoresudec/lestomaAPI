using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/modulos")]
    [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
    [ApiController]
    public class ModuloController : BaseController
    {
        private readonly IModuloService _moduloService;
        public ModuloController(IMapper mapper, IModuloService moduloService)
            : base(mapper)
        {
            _moduloService = moduloService;
        }

        [HttpGet("paginar")]
        public async Task<IActionResult> GetModulosPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _moduloService.GetAllAsQueryable();
            var listado = await GetPaginacion<EModuloComponente, ModuloDTO>(paginacion, queryable);
            var paginador = Paginador<ModuloDTO>.CrearPaginador(queryable.Count(), listado, paginacion);
            return Ok(paginador);
        }

        [HttpGet("listado")]
        public async Task<IActionResult> GetModulos()
        {
            var query = await _moduloService.GetAllAsync();
            var modulos = Mapear<IEnumerable<EModuloComponente>, IEnumerable<ModuloDTO>>(query);
            return Ok(modulos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModulo(int id)
        {
            var response = await _moduloService.GetByIdAsync(id);
            var moduloDTOSalida = Mapear<EModuloComponente, ModuloDTO>((EModuloComponente)response.Data);
            response.Data = moduloDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearModulo(ModuloRequest modulo)
        {
            var moduloDTO = Mapear<ModuloRequest, EModuloComponente>(modulo);
            var response = await _moduloService.CrearAsync(moduloDTO);
            var upaDTOSalida = Mapear<EModuloComponente, ModuloRequest>((EModuloComponente)response.Data);
            response.Data = upaDTOSalida;
            return CreatedAtAction(nameof(GetModulo), new { id = ((ModuloRequest)response.Data).Id }, response);
        }
        [HttpPut("editar")]
        public async Task<IActionResult> EditarModulo(ModuloRequest modulo)
        {
            var moduloDTO = Mapear<ModuloRequest, EModuloComponente>(modulo);
            var response = await _moduloService.ActualizarAsync(moduloDTO);
            var moduloDTOSalida = Mapear<EModuloComponente, ModuloRequest>((EModuloComponente)response.Data);
            response.Data = moduloDTOSalida;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarModulo(int id)
        {
            await _moduloService.EliminarAsync(id);
            return NoContent();
        }
    }
}
