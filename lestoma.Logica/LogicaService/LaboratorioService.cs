using lestoma.CommonUtils.DTOs;
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
        public LaboratorioService(IMailHelper mailHelper, UsuarioRepository usuarioRepository, LaboratorioRepository laboratorioRepository)
        {
            _mailHelper = mailHelper;
            _usuarioRepository = usuarioRepository;
            _laboratorioRepository = laboratorioRepository;
        }
        public async Task<ResponseDTO> CreateDetail(ELaboratorio detalle)
        {

            detalle.Id = Guid.NewGuid();
            await _laboratorioRepository.Create(detalle);
            return new ResponseDTO
            {
                IsExito = true,
                MensajeHttp = "Los datos offline fueron cargados con exito al servidor.",
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<ResponseDTO> BulkSyncDataOffline(IEnumerable<ELaboratorio> datosOffline)
        {
            await _laboratorioRepository.MergeDetails(datosOffline);
            return new ResponseDTO
            {
                IsExito = true,
                MensajeHttp = "Los datos offline fueron cargados con exito al servidor.",
                StatusCode = (int)HttpStatusCode.Created
            };
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

        public async Task<IEnumerable<DataComponentSyncDTO>> GetDataOfUserToSyncDeviceDatabase(Guid upaId)
        {
            if (upaId == Guid.Empty)
                throw new HttpStatusCodeException(HttpStatusCode.OK, $"No hay datos por migrar, usted no cuenta con una Upa asignada.");
            return await _laboratorioRepository.GetDataBySyncToMobileByUpaId(upaId);
        }

        public async Task<IEnumerable<NameDTO>> GetModulesByUpaActivitiesUserId(UpaActivitiesFilterRequest filtro)
        {
            return await _laboratorioRepository.GetModulesByUpaActivitiesUserId(filtro);
        }

        public async Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByModuleId(Guid id)
        {
            return await _laboratorioRepository.GetComponentsByModuleId(id);
        }
    }
}
