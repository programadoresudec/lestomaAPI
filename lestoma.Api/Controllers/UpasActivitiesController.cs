using AutoMapper;
using lestoma.Api.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/detalle-upas-actividades")]
    [ApiController]
    public class UpasActivitiesController : BaseController
    {
        private readonly IDetalleUpaActividadService _detalleService;
        public UpasActivitiesController(IMapper mapper, IDataProtectionProvider protectorProvider,
            IDetalleUpaActividadService upasActividadesService)
            : base(mapper, protectorProvider)
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

        [HttpPost("assign-to-a-user")]
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

        [HttpGet("listar-por-usuario")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> GetActividadesByUpaUser([FromQuery] UpaUserFilterRequest filtro)
        {
            if (!IsSuperAdmin() && filtro.UpaId == Guid.Empty && filtro.UsuarioId == 0)
            { 
                filtro.UpaId = UpaId();
                filtro.UsuarioId = UserIdDesencrypted();
            }
            var query = await _detalleService.GetActivitiesByUpaUserId(filtro);
            return Ok(query);
        }

        [HttpGet("listar-por-upa/{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetActividadesByUpa(Guid id)
        {
            var query = await _detalleService.GetActivitiesByUpaId(id);
            return Ok(query);
        }
    }
}
