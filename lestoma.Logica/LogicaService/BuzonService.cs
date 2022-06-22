using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Data.Repositories;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class BuzonService : IBuzonService
    {
        private readonly Response _respuesta = new();

        private BuzonRepository _buzonRepository;

        public BuzonService(BuzonRepository buzonRepository)
        {
            _buzonRepository = buzonRepository;
        }
        public async Task<List<EBuzon>> Listado()
        {
            return await _buzonRepository.ListarBuzonConUsuario();
        }
        public IQueryable<EBuzon> GetAllAsQueryable()
        {
            throw new NotImplementedException();
        }

        public async Task<Response> AgregarReporte(BuzonCreacionRequest buzonCreacion)
        {
            var reporte = new EBuzon
            {
                Descripcion = JsonSerializer.Serialize(buzonCreacion.Detalle),
                FechaCreacionServer = DateTime.Now,
                UsuarioId = buzonCreacion.UsuarioId
            };


            await _buzonRepository.Create(reporte);
            _respuesta.IsExito = true;
            _respuesta.Mensaje = "Enviado con exito.";
            return _respuesta;
        }

     
        public Task<EBuzon> GetBuzonById(int id)
        {
            return _buzonRepository.GetById(id);
        }

      
    }
}
