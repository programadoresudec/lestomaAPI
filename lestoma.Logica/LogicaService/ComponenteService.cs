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
    public class ComponenteService : IComponenteService
    {
        private ComponenteRepository _componenteRepo;
        private ActividadRepository _actividadRepo;
        private UpaRepository _upaRepo;
        private readonly Response _respuesta = new();

        public ComponenteService(ComponenteRepository componente, ActividadRepository _actividadRepository,
            UpaRepository upaRepository)
        {
            _componenteRepo = componente;
            _actividadRepo = _actividadRepository;
            _upaRepo = upaRepository;
        }
        public async Task<IEnumerable<EComponenteLaboratorio>> GetAll()
        {
            var listado = await _componenteRepo.GetAll();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido");
            }
            return listado;
        }
        public async Task<Response> Create(EComponenteLaboratorio entidad)
        {
            await Validaciones(entidad);
            entidad.Id = Guid.NewGuid();
            await _componenteRepo.Create(entidad);
            _respuesta.IsExito = true;
            _respuesta.Data = entidad;
            _respuesta.StatusCode = (int)HttpStatusCode.Created;
            _respuesta.Mensaje = "Se ha creado";
            return _respuesta;
        }

        private async Task Validaciones(EComponenteLaboratorio entidad)
        {
            var existeActividad = await _actividadRepo.AnyWithCondition(x => x.Id == entidad.ActividadId);
            if (!existeActividad)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la actividad.");
            }
            var existeUpa = await _upaRepo.AnyWithCondition(x => x.Id == entidad.UpaId);
            if (!existeUpa)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la upa.");
            }
            var existeModulo = await _actividadRepo.AnyWithCondition(x => x.Id == entidad.ActividadId);
            if (!existeModulo)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el modulo.");
            }
        }

        public List<NameDTO> GetComponentesJustNames()
        {
            return _componenteRepo.GetComponentesJustNames();

        }

        public async Task<Response> GetById(Guid id)
        {
            var query = await _componenteRepo.GetById(id);
            if (query == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.");
            _respuesta.Data = query;
            _respuesta.IsExito = true;
            _respuesta.Mensaje = "Encontrado";
            return _respuesta;
        }

        public async Task<Response> Update(EComponenteLaboratorio entidad)
        {
            var response = await GetById(entidad.Id);
            var comp = (EComponenteLaboratorio)response.Data;
            comp.NombreComponente = entidad.NombreComponente;
            await _componenteRepo.Update(comp);
            _respuesta.IsExito = true;
            _respuesta.StatusCode = (int)HttpStatusCode.OK;
            _respuesta.Mensaje = "se ha editado satisfactoriamente.";
            return _respuesta;
        }

        public async Task Delete(Guid id)
        {
            var entidad = await GetById(id);
            await _componenteRepo.Delete((EComponenteLaboratorio)entidad.Data);
        }



        public IQueryable<EComponenteLaboratorio> GetAllForPagination()
        {
            var listado = _componenteRepo.GetAllAsQueryable();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public IQueryable<ListadoComponenteDTO> GetAllFilter(Guid upaId)
        {
            var listado = _componenteRepo.GetAllFilter(upaId);
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }
    }


}



