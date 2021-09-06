using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("listadoactividades")]
        public async Task<IActionResult> ListaActividades()
        {
            var query = await _actividadService.ListaActividades();
            return Ok(query);
        }

    }
}
