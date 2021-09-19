using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
    [Route("api/upas")]
    [ApiController]
    public class UpaController : BaseController
    {
        private readonly IUpaService _upaService;
        public UpaController(IMapper mapper, IUpaService upaService)
            : base(mapper)
        {
            _upaService = upaService;
        }

        [HttpGet("paginar")]
        public async Task<IActionResult> GetUpasPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _upaService.ListaUpasPaginado();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.PageSize);
            var upas = await queryable.Paginar(paginacion).ToListAsync();
            var upaspaginado = Mapear<List<EUpa>, List<UpaRequest>>(upas);
            return Ok(upaspaginado);
        }
        [HttpGet("listado")]
        public async Task<IActionResult> GetUpas()
        {
            var query = await _upaService.ListaUpas();
            if (query.Count == 0)
            {
                return NoContent();
            }
            var upas = Mapear<List<EUpa>, List<UpaRequest>>(query);
            return Ok(upas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUpa(int id)
        {
            var response = await _upaService.GetUpa(id);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearUpa(UpaRequest upa)
        {
            var upaDTO = Mapear<UpaRequest, EUpa>(upa);
            var response = await _upaService.CrearUpa(upaDTO);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return CreatedAtAction(nameof(GetUpa), new { id = ((EUpa)response.Data).Id }, response);
        }


    }
}
