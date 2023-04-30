using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Data.Repositories;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class BuzonService : IBuzonService
    {
        private readonly IAuditoriaHelper _camposAuditoria;
        private readonly BuzonRepository _buzonRepository;

        public BuzonService(BuzonRepository buzonRepository, IAuditoriaHelper auditoriaHelper)
        {
            _buzonRepository = buzonRepository;
            _camposAuditoria = auditoriaHelper;
        }

        public IQueryable<BuzonDTO> GetAllForPagination(Guid UpaId)
        {
            return _buzonRepository.ListarBuzonConUsuario(UpaId);
        }

        public async Task<ResponseDTO> CreateMailBox(BuzonCreacionRequest buzonCreacion)
        {
            var reporte = new EBuzon
            {
                Descripcion = JsonSerializer.Serialize(buzonCreacion.Detalle),
                UsuarioId = buzonCreacion.UsuarioId,
                FechaCreacionServer = DateTime.Now,
                Ip = _camposAuditoria.GetDesencrytedIp(),
                Session = _camposAuditoria.GetSession(),
                TipoDeAplicacion = _camposAuditoria.GetTipoDeAplicacion(),
            };
            await _buzonRepository.Create(reporte);
            return Responses.SetCreatedResponse(reporte);
        }


        public async Task<ResponseDTO> GetMailBoxById(int id)
        {
            var data = await _buzonRepository.GetMailBoxById(id);
            return data == null
                ? throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.")
                : Responses.SetOkResponse(data);
        }
    }
}
