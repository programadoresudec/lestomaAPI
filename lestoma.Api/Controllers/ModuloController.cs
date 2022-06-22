using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lestoma.Api.Controllers
{
    [Route("api/modulo")]
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
        public async Task<IActionResult> GetModuloPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _moduloService.GetAllAsQueryable();
            var listado = await GetPaginacion<EModuloComponente, ModuloDTO>(paginacion, queryable);
            var paginador = Paginador<ModuloDTO>.CrearPaginador(queryable.Count(), listado, paginacion);
            return Ok(paginador);
        }
        [HttpGet("listado")]
        public async Task<IActionResult> GetModulo()
        {
            var query = await _moduloService.GetAllAsync();
            var upas = Mapear<List<EModuloComponente>, List<ModuloDTO>>(query.ToList());
            return Ok(modulo);
        }
        [HttpGet("listado-nombres")]
        public IActionResult GetModuloNombres()
        {
            var query = _moduloService.GetModuloJustNames();
            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModulo(int id)
        {
            var response = await _moduloService.GetByIdAsync(id);
            var moduloDTOSalida = Mapear<EModuloComponente, ModuloRequest>((EModuloComponente)response.Data);
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
        public async Task<IActionResult> EditarUpa(ModuloRequest modulo)
        {
            var moduloDTO = Mapear<ModuloRequest, EModuloComponente>(modulo);
            var response = await _moduloService.ActualizarAsync(moduloDTO);
            var upaDTOSalida = Mapear<EModuloComponente, ModuloRequest>((EModuloComponente)response.Data);
            response.Data = moduloDTOSalida;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUpa(Guid id)
        {
            await _moduloService.EliminarAsync(id);
            return NoContent();
        }
    }
}
