using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Data.Repositories;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class BuzonService : IBuzonService
    {
        private readonly ResponseDTO _respuesta = new();

        private BuzonRepository _buzonRepository;

        public BuzonService(BuzonRepository buzonRepository)
        {
            _buzonRepository = buzonRepository;
        }
        public IQueryable<BuzonDTO> GetAllForPagination()
        {
            return _buzonRepository.ListarBuzonConUsuario();
        }

        public async Task<ResponseDTO> CreateMailBox(BuzonCreacionRequest buzonCreacion)
        {
            var reporte = new EBuzon
            {
                Descripcion = JsonSerializer.Serialize(buzonCreacion.Detalle),
                UsuarioId = buzonCreacion.UsuarioId
            };
            await _buzonRepository.Create(reporte);
            _respuesta.IsExito = true;
            _respuesta.MensajeHttp = "Enviado con exito.";
            return _respuesta;
        }

     
        public async Task<EBuzon> GetMailBoxById(int id)
        {
            var data = await _buzonRepository.GetById(id);
            if (data == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return data;
        }
    }
}
