using lestoma.CommonUtils.Entities;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using lestoma.Data;
using lestoma.Data.DAO;
using lestoma.Logica.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LSBuzon : IBuzonService
    {
        private readonly Response _respuesta = new();
        private readonly Mapeo _db;
        private IGenericRepository<EBuzon> _buzonRepository;

        public LSBuzon(IGenericRepository<EBuzon> buzonRepository, Mapeo db)
        {
            _db = db;
            _buzonRepository = buzonRepository;
        }

        public async Task<Response> AgregarReporte(BuzonCreacionRequest buzonCreacion)
        {
            var reporte = new EBuzon
            {
                Descripcion = JsonSerializer.Serialize(buzonCreacion.Detalle),
                FechaCreacion = DateTime.Now,
                UsuarioId = buzonCreacion.UsuarioId
            };


            await _buzonRepository.Create(reporte);
            _respuesta.IsExito = true;
            _respuesta.Mensaje = "Enviado con exito.";
            return _respuesta;
        }

        public Task<EBuzon> GetBuzonById(int id)
        {
            return _buzonRepository.GetByIdAsync(id);
        }

        public async Task<List<EBuzon>> Listado()
        {
            return await new DAOBuzonReportes().ListarBuzonConUsuario(_db);
        }
    }
}
