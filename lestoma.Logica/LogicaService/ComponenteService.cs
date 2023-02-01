using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
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
        public async Task<ResponseDTO> Create(EComponenteLaboratorio entidad)
        {
            await Validaciones(entidad, true);
            entidad.Id = Guid.NewGuid();
            await _componenteRepo.Create(entidad);
            return Responses.SetCreatedResponse(entidad);
        }


        public async Task<IEnumerable<NameDTO>> GetComponentesJustNames()
        {
            return await _componenteRepo.GetComponentesJustNames();

        }

        public async Task<ResponseDTO> GetById(Guid id)
        {
            var query = await _componenteRepo.GetInfoById(id);
            if (query == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.");
            return Responses.SetOkResponse(query);
        }

        public async Task<ResponseDTO> Update(EComponenteLaboratorio entidad)
        {
            var componente = await _componenteRepo.GetById(entidad.Id);
            if (componente == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.");

            await Validaciones(entidad);
            componente.NombreComponente = entidad.NombreComponente;
            componente.ActividadId = entidad.ActividadId;
            componente.UpaId = entidad.UpaId;
            componente.ModuloComponenteId = entidad.ModuloComponenteId;
            if (!string.IsNullOrWhiteSpace(entidad.JsonEstadoComponente))
            {
                componente.JsonEstadoComponente = entidad.JsonEstadoComponente;
            }
            await _componenteRepo.Update(componente);
            return Responses.SetOkMessageEditResponse(componente);
        }

        public async Task Delete(Guid id)
        {
            var componente = await _componenteRepo.GetById(id);
            if (componente == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.");
            await _componenteRepo.Delete(componente);
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
        private async Task Validaciones(EComponenteLaboratorio entidad, bool IsCreated = false)
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
            if (IsCreated)
            {
                var existeRepetido = await _componenteRepo.AnyWithCondition(x => x.ActividadId == entidad.ActividadId && x.UpaId == entidad.UpaId
                                                                      && x.ModuloComponenteId == entidad.ModuloComponenteId && x.NombreComponente == entidad.NombreComponente);
                if (existeRepetido)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.Conflict, "Ya se encuentra registrado un componente con los mismos parametros.");
                }
            }
        }

    }


}



