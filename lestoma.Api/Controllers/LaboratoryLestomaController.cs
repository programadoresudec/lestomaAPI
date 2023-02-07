using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using AutoMapper;
using lestoma.Logica.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;

namespace lestoma.Api.Controllers
{
    [Route("api/laboratorio-lestoma")]
    [Authorize]
    [ApiController]
    public class LaboratoryLestomaController : BaseController
    {
        private readonly IDetalleUpaActividadService _detalleUpaActividadService;
        private readonly ILaboratorioService _laboratorioService;
        public LaboratoryLestomaController(IMapper mapper, IDataProtectionProvider protectorProvider,
            IDetalleUpaActividadService detalleUpaActividadService, ILaboratorioService laboratorioService)
            : base(mapper, protectorProvider)
        {
            _detalleUpaActividadService = detalleUpaActividadService;
            _laboratorioService = laboratorioService;
        }

        [HttpGet("listar-modulos-upa-actividad-por-usuario")]
        public async Task<IActionResult> GetModulosByUpaAndUserId()
        {
            var filtro1 = new UpaUserFilterRequest
            {
                UpaId = UpaId(),
                UsuarioId = UserIdDesencrypted()
            };

            var activities = await _detalleUpaActividadService.GetActivitiesByUpaUserId(filtro1);

            if (!activities.Any())
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El usuario no tiene actividades asignadas.");

            UpaActivitiesFilterRequest filtro = new UpaActivitiesFilterRequest
            {
                ActividadesId = activities.Select(x => x.Id).ToList(),
                UpaId = UpaId()
            };
            var data = await _laboratorioService.GetModulesByUpaActivitiesUserId(filtro);
            return Ok(data);
        }

        [HttpGet("listar-componentes-modulo/{Id}")]
        public async Task<IActionResult> GetComponentesByModuloId(Guid Id)
        {
            var data = await _laboratorioService.GetComponentsByModuleId(Id);
            return Ok(data);
        }
    }
}
