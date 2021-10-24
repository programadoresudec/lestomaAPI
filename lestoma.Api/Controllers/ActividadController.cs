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
    [Route("api/actividades")]
    [ApiController]
    public class ActividadController : BaseController
    {

        private readonly IGenericCRUD<EActividad> _actividadService;
        public ActividadController(IMapper mapper, IGenericCRUD<EActividad> actividadService)
            : base(mapper)
        {
            _actividadService = actividadService;
        }
        [HttpGet("listado")]
        public async Task<IActionResult> ListaActividades()
        {
            var query = await _actividadService.GetAll();
            var actividades = Mapear<List<EActividad>, List<ActividadRequest>>(query);
            return Ok(actividades);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getActividad(int id)
        {
            var response = await _actividadService.GetByIdAsync(id);
            var actividadDTOSalida = Mapear<EActividad, ActividadRequest>((EActividad)response.Data);
            response.Data = actividadDTOSalida;
            return Ok(response);
        }
        [HttpPost("crear")]
        public async Task<IActionResult> Crear(ActividadRequest actividad)
        {
            var objeto = Mapear<ActividadRequest, EActividad>(actividad);
            var response = await _actividadService.CrearAsync(objeto);
            return Created(string.Empty, response);
        }
        [HttpPut("editar")]
        public async Task<IActionResult> Editar(ActividadRequest actividad)
        {
            var objeto = Mapear<ActividadRequest, EActividad>(actividad);
            var response = await _actividadService.ActualizarAsync(objeto);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _actividadService.EliminarAsync(id);
            return NoContent();
        }

    }
}
