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
    [Route("api/detalle-upas-actividades")]
    [ApiController]
    public class UpasActividadesController : BaseController
    {
        private readonly IGenericCRUD<EUpaActividad> _service;
        public UpasActividadesController(IMapper mapper, IGenericCRUD<EUpaActividad> upasActividadesService)
            : base(mapper)
        {
            _service = upasActividadesService;
        }
        [HttpGet("paginar")]
        public async Task<IActionResult> GetUpasPaginado([FromQuery] Paginacion paginacion)
        {
            var listado = await _service.GetAll();
            List<DetalleUpaActividadDTO> detalleDTO = Mapear<List<EUpaActividad>, List<DetalleUpaActividadDTO>>(listado);
            var queryable = detalleDTO.Cast<DetalleUpaActividadDTO>().AsQueryable();
            var paginador = Paginador<DetalleUpaActividadDTO>.CrearPaginador(queryable, paginacion);
            return Ok(paginador);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearUpa(CrearDetalleUpaActividadRequest entidad)
        {
            var upaActividadDTO = Mapear<CrearDetalleUpaActividadRequest, EUpaActividad>(entidad);
            var response = await _service.CrearAsync(upaActividadDTO);

            return CreatedAtAction(null, response);
        }
    }
}
