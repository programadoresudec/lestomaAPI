using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using lestoma.Logica.LogicaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> GetDetallePaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _detalleService.GetAllForPagination();
            var listado = await queryable.Paginar(paginacion).ToListAsync();
            var paginador = Paginador<DetalleUpaActividadDTO>.CrearPaginador(listado.Count, listado, paginacion);
            return Ok(paginador);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearDetalle(CrearDetalleUpaActividadRequest entidad)
        {
            var upaActividadDTO = Mapear<CrearDetalleUpaActividadRequest, EUpaActividad>(entidad);
            var response = await _detalleService.CreateInCascade(upaActividadDTO);

            return CreatedAtAction(null, response);
        }

        [HttpPut("editar")]
        public async Task<IActionResult> EditarDetalle(CrearDetalleUpaActividadRequest entidad)
        {
            var upaActividadDTO = Mapear<CrearDetalleUpaActividadRequest, EUpaActividad>(entidad);
            var response = await _detalleService.UpdateInCascade(upaActividadDTO);

            return CreatedAtAction(null, response);
        }

        [HttpGet("lista-actividades-by-upa-usuario")]
        public async Task<IActionResult> GetActividadesByUpaUser([FromQuery] UpaUserFilterRequest filtro)
        {
            var query = await _detalleService.GetActivitiesByUpaUserId(filtro);
            return Ok(query);
        }

    }
}
