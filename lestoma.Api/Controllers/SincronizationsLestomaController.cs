using AutoMapper;
using Hangfire;
using lestoma.Api.Core;
using lestoma.CommonUtils.Enums;
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
    [Route("api/sincronizaciones-lestoma")]
    [Authorize]
    [ApiController]
    public class SincronizationsLestomaController : BaseController
    {

        private readonly ILaboratorioService _laboratorioService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IDetalleUpaActividadService _detalleUpaActividadService;
        #region Constructor
        public SincronizationsLestomaController(IMapper mapper, IDataProtectionProvider protectorProvider,
            ILaboratorioService laboratorioService, IBackgroundJobClient backgroundJobClient, IDetalleUpaActividadService detalleUpaActividadService)
            : base(mapper, protectorProvider)
        {
            _laboratorioService = laboratorioService;
            _backgroundJobClient = backgroundJobClient;
            _detalleUpaActividadService = detalleUpaActividadService;
        }
        #endregion


        [HttpGet("sync-data-online-to-database-device")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador, TipoRol.Auxiliar)]
        public async Task<IActionResult> SyncDeviceDatabase()
        {
            UpaActivitiesFilterRequest filtro = new();
            if (!IsSuperAdmin())
            {
                var UpaUserFilter = new UpaUserFilterRequest
                {
                    UpaId = UpaId(),
                    UsuarioId = UserIdDesencrypted()
                };

                var activities = await _detalleUpaActividadService.GetActivitiesByUpaUserId(UpaUserFilter);

                if (!activities.Any())
                    throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El usuario no tiene actividades asignadas.");

                filtro = new UpaActivitiesFilterRequest
                {
                    ActividadesId = activities.Select(x => x.Id).ToList(),
                    UpaId = UpaId()
                };
            }
            var data = await _laboratorioService.GetDataOfUserToSyncDeviceDatabase(filtro, IsSuperAdmin(), IsAuxiliar());
            return Ok(data);
        }


        [HttpPost("bulk-sync-data-offline")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador, TipoRol.Auxiliar)]
        public IActionResult SyncLabDataOffline(IEnumerable<LaboratorioRequest> datosOfOffline)
        {
            var EmailDesencryptedUser = EmailDesencrypted();
            var datosMapeados = Mapear<IEnumerable<LaboratorioRequest>, IEnumerable<ELaboratorio>>(datosOfOffline);
            var jobId = _backgroundJobClient.Schedule<ILaboratorioService>(service => service.BulkSyncDataOffline(datosMapeados), TimeSpan.FromSeconds(10));
            _backgroundJobClient.ContinueJobWith<ILaboratorioService>(jobId, service => service.SendEmailFinishMerge(EmailDesencryptedUser));
            return Accepted(Responses.SetAcceptedResponse(null,
                "Se esta generando la migración de datos, pronto le llegará un correo cuando termine."));
        }
    }
}
