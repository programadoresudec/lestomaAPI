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
    [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
    [Route("api/upas")]
    [ApiController]
    public class UpaController : BaseController
    {
        private readonly IGenericCRUD<EUpa> _upaService;
        public UpaController(IMapper mapper, IGenericCRUD<EUpa> upaService)
            : base(mapper)
        {
            _upaService = upaService;
        }

        [HttpGet("paginar")]
        public async Task<IActionResult> GetUpasPaginado([FromQuery] Paginacion paginacion)
        {
            var listado = await _upaService.GetAll();
            var upas = Mapear<List<EUpa>, List<UpaDTO>>(listado);
            var queryable = upas.Cast<UpaDTO>().AsQueryable();
            var paginador = Paginador<UpaDTO>.CrearPaginador(queryable, paginacion);
            return Ok(paginador);
        }
        [HttpGet("listado")]
        public async Task<IActionResult> GetUpas()
        {
            var query = await _upaService.GetAll();
            var upas = Mapear<List<EUpa>, List<UpaDTO>>(query);
            return Ok(upas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUpa(int id)
        {
            var response = await _upaService.GetByIdAsync(id);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearUpa(UpaRequest upa)
        {
            var upaDTO = Mapear<UpaRequest, EUpa>(upa);
            var response = await _upaService.CrearAsync(upaDTO);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return CreatedAtAction(nameof(GetUpa), new { id = ((UpaRequest)response.Data).Id }, response);
        }
        [HttpPut("editar")]
        public async Task<IActionResult> EditarUpa(UpaRequest upa)
        {
            var upaDTO = Mapear<UpaRequest, EUpa>(upa);
            var response = await _upaService.ActualizarAsync(upaDTO);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUpa(int id)
        {
            await _upaService.EliminarAsync(id);
            return NoContent();
        }
    }
}
