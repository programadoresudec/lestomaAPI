using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class DetalleUpaActividadService : IDetalleUpaActividadService

    {
        private readonly Response _respuesta = new();

        private readonly UpaActividadRepository _upasActividadesRepository;
        public DetalleUpaActividadService(UpaActividadRepository upasActividadesRepository)
        {
            _upasActividadesRepository = upasActividadesRepository;
        }

        public Task<Response> ActualizarEnCascada(EUpaActividad entidad)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response> CrearEnCascada(EUpaActividad entidad)
        {

            await _upasActividadesRepository.CreateRelation(entidad);
            return new Response
            {
                IsExito = true,
                Mensaje = "Se ha creado satisfactoriamente.",
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public Task EliminarEnCascada(int IdUsuario)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<EUpaActividad>> GetAll()
        {
            return await _upasActividadesRepository.GetAll();
        }

        public IQueryable<EUpaActividad> GetAllAsQueryable()
        {
            var query = _upasActividadesRepository.GetAllRelation();
            if (!query.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay actividades.");
            }
            return query;
        }
    }
}
