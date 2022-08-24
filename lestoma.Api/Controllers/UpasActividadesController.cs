using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
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
            var listado = await GetPaginacion<EUpaActividad, DetalleUpaActividadDTO>(paginacion, queryable);
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
    }
}
