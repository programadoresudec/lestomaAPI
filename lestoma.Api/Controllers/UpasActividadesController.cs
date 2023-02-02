using AutoMapper;
using lestoma.Api.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/detalle-upas-actividades")]
    [ApiController]
    public class UpasActividadesController : BaseController
    {
        private readonly IDetalleUpaActividadService _detalleService;
        public UpasActividadesController(IMapper mapper, IDetalleUpaActividadService upasActividadesService)
            : base(mapper)
        {
            _detalleService = upasActividadesService;
        }

        [HttpGet("paginar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetDetallePaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _detalleService.GetAllForPagination();
            var listado = await queryable.Paginar(paginacion).ToListAsync();
            var paginador = Paginador<DetalleUpaActividadDTO>.CrearPaginador(listado.Count, listado, paginacion);
            return Ok(paginador);
        }

        [HttpPost("crear")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> CrearDetalle(CrearDetalleUpaActividadRequest entidad)
        {
            var upaActividadDTO = Mapear<CrearDetalleUpaActividadRequest, EUpaActividad>(entidad);
            var response = await _detalleService.CreateInCascade(upaActividadDTO);
            return CreatedAtAction(null, response);
        }

        [HttpPut("editar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> EditarDetalle(CrearDetalleUpaActividadRequest entidad)
        {
            var upaActividadDTO = Mapear<CrearDetalleUpaActividadRequest, EUpaActividad>(entidad);
            var response = await _detalleService.UpdateInCascade(upaActividadDTO);
            return Ok(response);
        } 

        [HttpGet("listar-actividades-upa-usuario")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetActividadesByUpaUser([FromQuery] UpaUserFilterRequest filtro)
        {
            var query = await _detalleService.GetActivitiesByUpaUserId(filtro);
            return Ok(query);
        }

        [HttpGet("listar-actividades-by-upa/{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetActividadesByUpa(Guid id)
        {
            var query = await _detalleService.GetActivitiesByUpaId(id);
            return Ok(query);
        }
    }
}
