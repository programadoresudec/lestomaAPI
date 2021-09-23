using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : BaseController
    {

        private readonly IActividadService _actividadService;
        public ActividadController(IMapper mapper, IActividadService actividadService)
            : base(mapper)
        {
            _actividadService = actividadService;
        }
        [HttpGet("listado")]
        public async Task<IActionResult> ListaActividades()
        {
            var query = await _actividadService.ListaActividades();
            var actividades = Mapear<List<EActividad>, List<ActividadRequest>>(query);
            return Ok(actividades);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear(ActividadRequest actividad)
        {
            var objeto = Mapear<ActividadRequest, EActividad>(actividad);

            var response = await _actividadService.CrearActividad(objeto);

            return Created(string.Empty, response);
        }

    }
}
