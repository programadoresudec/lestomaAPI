using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.MyException;
using lestoma.Data.Repositories;
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
    public class ActividadService : IActividadService
    {
        private readonly ActividadRepository _actividadRepository;
        public ActividadService(ActividadRepository actividadRepository)
        {
            _actividadRepository = actividadRepository;
        }

        public async Task<IEnumerable<EActividad>> GetAll()
        {
            var query = await _actividadRepository.GetAllAsQueryable().OrderBy(y => y.Nombre).ToListAsync();
            return !query.Any() ? throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay actividades.") : query;
        }

        public IQueryable<EActividad> GetAllForPagination()
        {
            var query = _actividadRepository.GetAllAsQueryable().OrderBy(y => y.Nombre);
            return !query.Any() ? throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay actividades.") : query;
        }
        public async Task<ResponseDTO> GetById(Guid id)
        {
            var actividad = await _actividadRepository.GetById(id);
            return actividad == null
                ? throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la actividad.")
                : Responses.SetOkResponse(actividad);
        }
        public async Task<ResponseDTO> Create(EActividad entidad)
        {
            bool existe = await _actividadRepository.ExistActivity(entidad.Nombre, Guid.Empty);
            if (existe)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya está en uso utilice otro.");

            entidad.Id = Guid.NewGuid();
            await _actividadRepository.Create(entidad);
            return Responses.SetCreatedResponse(entidad);
        }
        public async Task<ResponseDTO> Update(EActividad entidad)
        {
            var response = await GetById(entidad.Id);
            var actividad = (EActividad)response.Data;
            bool existe = await _actividadRepository.ExistActivity(entidad.Nombre, actividad.Id, true);
            if (existe)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya está en uso utilice otro.");
            actividad.Nombre = entidad.Nombre;
            await _actividadRepository.Update(actividad);
            return Responses.SetOkMessageEditResponse();
        }

        public async Task Delete(Guid id)
        {
            var entidad = await GetById(id);
            await _actividadRepository.Delete((EActividad)entidad.Data);
        }

        public async Task<IEnumerable<NameDTO>> GetActividadesJustNames()
        {
            return await _actividadRepository.GetActivitiesJustNames();
        }
    }
}
