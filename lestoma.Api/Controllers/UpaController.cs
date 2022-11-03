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
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetUpasPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _upaService.GetAllForPagination();
            var listado = await GetPaginacion<EUpa, UpaDTO>(paginacion, queryable);
            var paginador = Paginador<UpaDTO>.CrearPaginador(queryable.Count(), listado, paginacion);
            return Ok(paginador);
        }
        [HttpGet("listado")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetUpas()
        {
            var query = await _upaService.GetAll();
            var upas = Mapear<List<EUpa>, List<UpaDTO>>(query.ToList());
            return Ok(upas);
        }
        [HttpGet("listado-nombres")]

        public IActionResult GetUpasNombres()
        {
            var query = _upaService.GetUpasJustNames();
            return Ok(query);
        }

        [HttpGet("{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetUpa(Guid id)
        {
            var response = await _upaService.GetById(id);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> CrearUpa(UpaRequest upa)
        {
            var upaDTO = Mapear<UpaRequest, EUpa>(upa);
            var response = await _upaService.Create(upaDTO);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return CreatedAtAction(nameof(GetUpa), new { id = ((UpaRequest)response.Data).Id }, response);
        }
        [HttpPut("editar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> EditarUpa(UpaRequest upa)
        {
            var upaDTO = Mapear<UpaRequest, EUpa>(upa);
            var response = await _upaService.Update(upaDTO);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> EliminarUpa(Guid id)
        {
            await _upaService.Delete(id);
            return NoContent();
        }
    }
}
