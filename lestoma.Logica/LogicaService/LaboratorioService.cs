using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.DTOs.Sync;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Data.Repositories;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LaboratorioService : ILaboratorioService
    {
        private readonly IMailHelper _mailHelper;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly LaboratorioRepository _laboratorioRepository;
        private readonly IAuditoriaHelper _camposAuditoria;
        private readonly ILoggerManager _logger;
        private readonly ComponenteRepository _componenteRepository;
        public LaboratorioService(IMailHelper mailHelper, UsuarioRepository usuarioRepository, ComponenteRepository componenteRepository,
            LaboratorioRepository laboratorioRepository, IAuditoriaHelper auditoria, ILoggerManager logger)
        {
            _camposAuditoria = auditoria;
            _mailHelper = mailHelper;
            _usuarioRepository = usuarioRepository;
            _laboratorioRepository = laboratorioRepository;
            _logger = logger;
            _componenteRepository = componenteRepository;
        }

        public async Task<ResponseDTO> CreateDetail(ELaboratorio detalle)
        {
            var existeComponent = await _componenteRepository.AnyWithCondition(x => x.Id == detalle.ComponenteLaboratorioId);
            if (!existeComponent)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encontro el componente.");
            detalle.Id = Guid.NewGuid();
            detalle.Session = _camposAuditoria.GetSession();
            detalle.Ip = string.IsNullOrWhiteSpace(detalle.Ip) ? _camposAuditoria.GetDesencrytedIp() : detalle.Ip;
            detalle.FechaCreacionServer = DateTime.Now;
            detalle.ValorCalculadoTramaEnviada = detalle.ValorCalculadoTramaEnviada.HasValue ?
                Reutilizables.TruncateDouble(detalle.ValorCalculadoTramaEnviada.Value, 2) : null;
            detalle.ValorCalculadoTramaRecibida = detalle.ValorCalculadoTramaRecibida.HasValue ?
               Reutilizables.TruncateDouble(detalle.ValorCalculadoTramaRecibida.Value, 2) : null;
            detalle.TipoDeAplicacion = _camposAuditoria.GetTipoDeAplicacion();
            await _laboratorioRepository.Create(detalle);
            return Responses.SetCreatedResponse(detalle);
        }

        public async Task<ResponseDTO> BulkSyncDataOffline(IEnumerable<ELaboratorio> datosOffline)
        {
            try
            {
                foreach (var item in datosOffline)
                {
                    item.FechaCreacionServer = DateTime.Now;
                    item.ValorCalculadoTramaEnviada = item.ValorCalculadoTramaEnviada.HasValue ?
                Reutilizables.TruncateDouble(item.ValorCalculadoTramaEnviada.Value, 2) : null;
                    item.ValorCalculadoTramaRecibida = item.ValorCalculadoTramaRecibida.HasValue ?
             Reutilizables.TruncateDouble(item.ValorCalculadoTramaRecibida.Value, 2) : null;
                }
                await _laboratorioRepository.MergeDetails(datosOffline);
                return Responses.SetOkResponse("Los datos offline fueron cargados con exito al servidor.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Los datos offline no se migro al servidor.", ex);
                throw;
            }
        }

        public async Task SendEmailFinishMerge(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, $"Email invalido.");

            var existe = _usuarioRepository.WhereWithCondition(x => x.Email.Equals(email)).Select(x => new
            {
                x.Email,
                NombreCompleto = x.Nombre + " " + x.Apellido
            }).FirstOrDefault();
            if (existe == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, @$"No se encontro la persona con el email: {email}, y no se pudo enviar 
                                                            la notificación para dar por terminado la migración de datos offline al servidor.");

            await _mailHelper.SendMail(existe.Email, "Finalización de migración de datos.", string.Empty,
                $"Hola: {existe.NombreCompleto}",
                 "Ha finalizado la migración de datos.",
                string.Empty, "Si no has intentado migrar datos offline al servidor, puedes ignorar este mensaje.");

        }

        public async Task<IEnumerable<DataOnlineSyncDTO>> GetDataOfUserToSyncDeviceDatabase(UpaActivitiesFilterRequest filtro, bool isSuperAdmin, bool isAuxiliar)
        {
            return await _laboratorioRepository.GetDataBySyncToMobileByUpaId(filtro, isSuperAdmin, isAuxiliar);
        }

        public async Task<IEnumerable<NameDTO>> GetModulesByUpaActivitiesUserId(UpaActivitiesFilterRequest filtro, bool IsAuxiliar)
        {
            return await _laboratorioRepository.GetModulesByUpaActivitiesUserId(filtro, IsAuxiliar);
        }

        public async Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByUpaAndModuleId(UpaModuleFilterRequest filtro)
        {
            return await _laboratorioRepository.GetComponentsByUpaAndModuleId(filtro);
        }

        public async Task<ResponseDTO> GetComponentRecentTrama(Guid id)
        {
            var existe = await _componenteRepository.AnyWithCondition(x => x.Id == id);
            if (!existe)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encontro el componente.");
            var data = await _laboratorioRepository.GetComponentRecentTrama(id);

            return Responses.SetOkResponse(data);
        }

        public async Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByActivitiesOfUpaUserId(UpaActivitiesModuleFilterRequest filtro, bool IsAuxiliar)
        {
            return await _laboratorioRepository.GetComponentsByActivitiesOfUpaUserId(filtro, IsAuxiliar);
        }

    }
}
