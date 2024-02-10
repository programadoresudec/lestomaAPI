using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Data.Repositories;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class BuzonService : IBuzonService
    {
        private readonly BuzonRepository _buzonRepository;
        private readonly IMailHelper _mailHelper;
        private readonly UsuarioRepository _usuarioRepository;
        public BuzonService(BuzonRepository buzonRepository, IMailHelper mailHelper, UsuarioRepository usuarioRepository)
        {
            _mailHelper = mailHelper;
            _buzonRepository = buzonRepository;
            _usuarioRepository = usuarioRepository;
        }

        public IQueryable<BuzonDTO> GetAllForPagination(Guid UpaId)
        {
            return _buzonRepository.ListarBuzonConUsuario(UpaId);
        }

        public async Task<ResponseDTO> CreateMailBox(BuzonCreacionRequest buzonCreacion)
        {
            buzonCreacion.Detalle.Estado = new EstadoBuzonDTO
            {
                Id = (int)TipoEstadoBuzon.Pendiente,
                Nombre = EnumConfig.GetDescription(TipoEstadoBuzon.Pendiente)
            };
            var reporte = new EBuzon
            {
                Descripcion = JsonSerializer.Serialize(buzonCreacion.Detalle),
                UsuarioId = buzonCreacion.UsuarioId
            };
            await _buzonRepository.Create(reporte);
            return Responses.SetCreatedResponse(reporte);
        }
        public async Task<ResponseDTO> EditStatusMailBox(EditarEstadoBuzonRequest buzonEdit)
        {
            var data = await _buzonRepository.GetById(buzonEdit.BuzonId)
                ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No existe el buzón.");

            var json = JsonSerializer.Deserialize<DetalleBuzonDTO>(data.Descripcion)
                ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No contiene datos el json.");

            json.Estado = buzonEdit.EstadoBuzon;

            data.Descripcion = JsonSerializer.Serialize(json);
            await _buzonRepository.Update(data);

            if (buzonEdit.EstadoBuzon.Id == (int)TipoEstadoBuzon.Finalizado)
            {
                string emailAuxiliar = await _usuarioRepository.WhereWithCondition(x => x.Id == data.UsuarioId).Select(y => y.Email).FirstOrDefaultAsync();

                await _mailHelper.SendMail(emailAuxiliar, $"El buzón {json.Titulo} solucionado.", String.Empty,
                    "Hola: ¡Auxiliar!",
                    $"Tu Buzón de la fecha {data.FechaCreacionServer} con título {json.Titulo} y una descripción {json.Descripcion} fue finalizado con exito.",
                    string.Empty, $"LESTOMA APP", true);
            }

            return Responses.SetOkMessageEditResponse(data);
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
