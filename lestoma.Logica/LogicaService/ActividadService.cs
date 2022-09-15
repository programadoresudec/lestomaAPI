using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
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
    public class ActividadService : IActividadService
    {
        private readonly Response _respuesta = new();

        private readonly ActividadRepository _actividadRepository;
        public ActividadService(ActividadRepository actividadRepository)
        {
            _actividadRepository = actividadRepository;
        }
        public async Task<IEnumerable<EActividad>> GetAll()
        {
            var query = await _actividadRepository.GetAll();
            if (query.Count() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay actividades.");
            }
            return query;
        }

        public IQueryable<EActividad> GetAllForPagination()
        {
            var query = _actividadRepository.GetAllAsQueryable();
            int variable = query.Count();
            if (variable == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay actividades.");
            }
            return query;
        }
        public async Task<Response> GetById(Guid id)
        {
            var query = await _actividadRepository.GetById(id);
            if (query != null)
            {
                _respuesta.Data = query;
                _respuesta.IsExito = true;
                _respuesta.Mensaje = "Encontrado";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la actividad.");
            }
            return _respuesta;
        }
        public async Task<Response> Create(EActividad entidad)
        {
            bool existe = await _actividadRepository.ExistActivity(entidad.Nombre, Guid.Empty);
            if (!existe)
            {
                entidad.Id = Guid.NewGuid();
                await _actividadRepository.Create(entidad);
                _respuesta.IsExito = true;
                _respuesta.StatusCode = (int)HttpStatusCode.Created;
                _respuesta.Mensaje = "se ha creado satisfactoriamente.";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya está en uso utilice otro.");
            }
            return _respuesta;

        }
        public async Task<Response> Update(EActividad entidad)
        {
            var response = await GetById(entidad.Id);
            var actividad = (EActividad)response.Data;
            bool existe = await _actividadRepository.ExistActivity(entidad.Nombre, actividad.Id, true);
            if (!existe)
            {
                actividad.Nombre = entidad.Nombre;
                await _actividadRepository.Update(actividad);
                _respuesta.IsExito = true;
                _respuesta.StatusCode = (int)HttpStatusCode.OK;
                _respuesta.Mensaje = "se ha editado satisfactoriamente.";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya está en uso utilice otro.");
            }

            return _respuesta;
        }

        public async Task Delete(Guid id)
        {
            var entidad = await GetById(id);
            await _actividadRepository.Delete((EActividad)entidad.Data);
        }

        public List<NameDTO> GetActividadesJustNames()
        {
            return _actividadRepository.GetActivitiesJustNames();
        }
    }
}
