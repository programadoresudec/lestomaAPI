using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Interfaces;
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
        public async Task<Response> CreateDetail(ELaboratorio detalle)
        {

            detalle.Id = Guid.NewGuid();
            await _laboratorioRepository.Create(detalle);
            return new Response
            {
                IsExito = true,
                Mensaje = "Los datos offline fueron cargados con exito al servidor.",
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> MergeDetails(IEnumerable<ELaboratorio> datosOffline)
        {
            await _laboratorioRepository.MergeDetails(datosOffline);
            return new Response
            {
                IsExito = true,
                Mensaje = "Los datos offline fueron cargados con exito al servidor.",
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task SendEmailFinishMerge(string email)
        {
            var existe = _usuarioRepository.WhereWithCondition(x => x.Email.Equals(email)).Select(x => new
            { Email = x.Email, NombreCompleto = x.Nombre + " " + x.Apellido }).FirstOrDefault();
            if (existe != null)
            {
                await _mailHelper.SendMail(existe.Email, "Finalización de migración de datos.", string.Empty,
                $"Hola: {existe.NombreCompleto}",
                 "Ha finalizado la migración de datos.",
                string.Empty, "Si no has intentado migrar datos offline al servidor, puedes ignorar este mensaje.");
            }

        }
    }
}
