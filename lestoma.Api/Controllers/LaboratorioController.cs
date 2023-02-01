using AutoMapper;
using Hangfire;
using lestoma.CommonUtils.Helpers;
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
    public class LaboratorioController : BaseController
    {

        private readonly ILaboratorioService _laboratorioService;
        private readonly IDetalleUpaActividadService _detalleUpaActividad;
        private readonly IBackgroundJobClient _backgroundJobClient;
        #region Constructor
        public LaboratorioController(IMapper mapper, ILaboratorioService laboratorioService,
            IBackgroundJobClient backgroundJobClient, IDataProtectionProvider protectorProvider, IDetalleUpaActividadService detalleUpaActividad)
            : base(mapper, protectorProvider)
        {
            _laboratorioService = laboratorioService;
            _backgroundJobClient = backgroundJobClient;
            _detalleUpaActividad = detalleUpaActividad;
        }
        #endregion

        [HttpGet("listado")]
        public async Task<IActionResult> GetDetalle()
        {
            return Ok();

        }

        [HttpGet("listar-modulos-upa-actividad-por-usuario")]
        public async Task<IActionResult> GetModulosByUpaAndUserId()
        {
            var filtro1 = new UpaUserFilterRequest
            {
                UpaId = UpaId(),
                UsuarioId = UserIdDesencrypted()
            };

            var activities = await _detalleUpaActividad.GetActivitiesByUpaUserId(filtro1);

            if (!activities.Any())
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El usuario no tiene actividades asignadas.");

            UpaActivitiesFilterRequest filtro = new UpaActivitiesFilterRequest
            {
                ActividadesId = activities.Select(x => x.Id).ToList(),
                UpaId = UpaId()
            };
            var data = await _laboratorioService.GetModulosByUpaAndActivitiesOfUser(filtro);
            return Ok(data);
        }

        [HttpGet("listar-componentes-modulo/{Id}")]
        public async Task<IActionResult> GetComponentesByModuloId(Guid Id)
        {
            var data = await _laboratorioService.GetComponentsByModuleId(Id);
            return Ok(data);
        }

        [HttpGet("data-sincronizada-modo-offline-upa")]
        public async Task<IActionResult> GetDataBySyncToMobileByUpaId()
        {
            var upaId = UpaId();
            var data = await _laboratorioService.GetDataBySyncToMobileByUpaId(upaId);
            return Ok(data);
        }


        [HttpPost("sincronizar-data-modo-offline")]
        public IActionResult SyncLabDataOffline(IEnumerable<LaboratorioRequestOffline> datosOfOffline)
        {
            var EmailDesencryptedUser = EmailDesencrypted();
            var datosMapeados = Mapear<IEnumerable<LaboratorioRequestOffline>, IEnumerable<ELaboratorio>>(datosOfOffline);
            var jobId = _backgroundJobClient.Schedule<ILaboratorioService>(service =>
           service.SyncLabDataOffline(datosMapeados), TimeSpan.FromSeconds(10));
            _backgroundJobClient.ContinueJobWith<ILaboratorioService>(jobId, service => service.SendEmailFinishMerge(EmailDesencryptedUser));
            return Accepted(Responses.SetAcceptedResponse(null,
                "Se esta generando la migración de datos, pronto le llegará un correo cuando termine."));
        }
    }
}
