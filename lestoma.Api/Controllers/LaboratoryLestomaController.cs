using AutoMapper;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/laboratorio-lestoma")]
    [Authorize]
    [ApiController]
    public class LaboratoryLestomaController : BaseController
    {
        private readonly IDetalleUpaActividadService _detalleUpaActividadService;
        private readonly ILaboratorioService _laboratorioService;
        private readonly IModuloService _moduloService;
        public LaboratoryLestomaController(IMapper mapper, IDataProtectionProvider protectorProvider,
            IDetalleUpaActividadService detalleUpaActividadService, ILaboratorioService laboratorioService, IModuloService moduloService)
            : base(mapper, protectorProvider)
        {
            _detalleUpaActividadService = detalleUpaActividadService;
            _laboratorioService = laboratorioService;
            _moduloService = moduloService;
        }

        [HttpGet("listar-modulos-upa-actividad-por-usuario")]
        public async Task<IActionResult> GetModulosByUpaAndUserId()
        {
            IEnumerable<NameDTO> data;
            if (IsSuperAdmin())
            {
                data = await _moduloService.GetModulosJustNames();
                return Ok(data);
            }
            UpaUserFilterRequest UpaUserFilter = new()
            {
                UpaId = UpaId(),
                UsuarioId = UserIdDesencrypted()
            };

            var activities = await _detalleUpaActividadService.GetActivitiesByUpaUserId(UpaUserFilter);

            if (!activities.Any())
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El usuario no tiene actividades asignadas.");

            UpaActivitiesFilterRequest filtro = new()
            {
                ActividadesId = activities.Select(x => x.Id),
                UpaId = UpaUserFilter.UpaId
            };


            data = await _laboratorioService.GetModulesByUpaActivitiesUserId(filtro, IsAuxiliar());

            return Ok(data);
        }

        [HttpGet("listar-componentes-upa-modulo")]
        public async Task<IActionResult> GetComponentesByUpaAndModuloId([FromQuery] UpaModuleFilterRequest filtro)
        {
            IEnumerable<LaboratorioComponenteDTO> data;    
            if (IsSuperAdmin())
            {
                data = await _laboratorioService.GetComponentsByUpaAndModuleId(filtro);
                return Ok(data);
            }

            UpaUserFilterRequest UpaUserFilter = new()
            {
                UpaId = UpaId(),
                UsuarioId = UserIdDesencrypted()
            };
            var activities = await _detalleUpaActividadService.GetActivitiesByUpaUserId(UpaUserFilter);

            if (!activities.Any())
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El usuario no tiene actividades asignadas.");

            UpaActivitiesModuleFilterRequest upaActivitiesModuleFilterRequest = new()
            {
                ActividadesId = activities.Select(x => x.Id),
                ModuloId = filtro.ModuloId,
                UpaId = UpaUserFilter.UpaId
            };

            data = await _laboratorioService.GetComponentsByActivitiesOfUpaUserId(upaActivitiesModuleFilterRequest, IsAuxiliar());
            return Ok(data);

        }


        [HttpGet("ultimo-registro-componente/{id}")]
        public async Task<IActionResult> GetComponentesByUpaAndModuloId(Guid id)
        {
            var data = await _laboratorioService.GetComponentRecentTrama(id);
            return Ok(data);
        }

        [HttpPost("crear-detalle")]
        public async Task<IActionResult> AddDetail(LaboratorioRequest laboratorioRequest)
        {
            var datoMapeado = Mapear<LaboratorioRequest, ELaboratorio>(laboratorioRequest);
            var response = await _laboratorioService.CreateDetail(datoMapeado);
            return CreatedAtAction(null, response);
        }
    }
}
