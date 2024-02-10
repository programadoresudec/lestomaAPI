using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
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
    public class ComponenteService : IComponenteService
    {
        private readonly ComponenteRepository _componenteRepository;
        private readonly ActividadRepository _actividadRepository;
        private readonly UpaRepository _upaRepository;
        private readonly ModuloRepository _moduloRepository;

        public ComponenteService(ComponenteRepository componenteRepository, ActividadRepository actividadRepository,
            UpaRepository upaRepository, ModuloRepository moduloRepository)
        {
            _componenteRepository = componenteRepository;
            _actividadRepository = actividadRepository;
            _upaRepository = upaRepository;
            _moduloRepository = moduloRepository;
        }

        public async Task<IEnumerable<EComponenteLaboratorio>> GetAll()
        {
            var listado = await _componenteRepository.GetAll();
            return !listado.Any() ? throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido") : listado;
        }
        public async Task<ResponseDTO> Create(EComponenteLaboratorio entidad)
        {
            await Validations(entidad, true);
            entidad.Id = Guid.NewGuid();
            await _componenteRepository.Create(entidad);
            return Responses.SetCreatedResponse(entidad);
        }

        public async Task<IEnumerable<NameDTO>> GetComponentsJustNames()
        {
            return await _componenteRepository.GetComponentesJustNames();
        }

        public async Task<ResponseDTO> GetById(Guid id)
        {
            var componente = await _componenteRepository.GetInfoById(id);
            return componente == null
                ? throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.")
                : Responses.SetOkResponse(componente);
        }

        public async Task<ResponseDTO> Update(EComponenteLaboratorio entidad)
        {
            var componente = await _componenteRepository.GetById(entidad.Id);
            if (componente == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.");

            await Validations(entidad);
            componente.NombreComponente = entidad.NombreComponente;
            componente.ActividadId = entidad.ActividadId;
            componente.UpaId = entidad.UpaId;
            componente.DireccionRegistro = entidad.DireccionRegistro;
            componente.ModuloComponenteId = entidad.ModuloComponenteId;
            if (!string.IsNullOrWhiteSpace(entidad.JsonEstadoComponente))
            {
                componente.JsonEstadoComponente = entidad.JsonEstadoComponente;
            }
            await _componenteRepository.Update(componente);
            return Responses.SetOkMessageEditResponse(componente);
        }

        public async Task Delete(Guid id)
        {
            var componente = await _componenteRepository.GetById(id);
            if (componente == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.");
            await _componenteRepository.Delete(componente);
        }

        public IQueryable<EComponenteLaboratorio> GetAllForPagination()
        {
            var listado = _componenteRepository.GetAllAsQueryable();
            return !listado.Any() ? throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.") : listado;
        }

        public IQueryable<ListadoComponenteDTO> GetAllFilter(UpaActivitiesFilterRequest upaActivitiesFilter)
        {
            var listado = _componenteRepository.GetAllFilter(upaActivitiesFilter);
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }
        private async Task Validations(EComponenteLaboratorio entidad, bool IsCreated = false)
        {
            bool existeActividad = await _actividadRepository.AnyWithCondition(x => x.Id == entidad.ActividadId);
            if (!existeActividad)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la actividad.");
            }
            bool existeUpa = await _upaRepository.AnyWithCondition(x => x.Id == entidad.UpaId);
            if (!existeUpa)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la upa.");
            }
            bool existeModulo = await _moduloRepository.AnyWithCondition(x => x.Id == entidad.ModuloComponenteId);
            if (!existeModulo)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el modulo.");
            }

            bool existeRepetido;
            if (IsCreated)
            {
                existeRepetido = await _componenteRepository.AnyWithCondition(x => x.UpaId == entidad.UpaId && x.NombreComponente
                                                                      == entidad.NombreComponente);
            }
            else
            {
                existeRepetido = await _componenteRepository.AnyWithCondition(x => x.UpaId == entidad.UpaId && x.NombreComponente
                                                                      == entidad.NombreComponente && x.Id != entidad.Id);
            }

            if (existeRepetido)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, $"Ya existe un componente {entidad.NombreComponente} de la upa.");
            }

            if (IsCreated)
            {
                if (entidad.ObjetoJsonEstado.TipoEstado == EnumConfig.GetDescription(TipoEstadoComponente.Lectura)
                         || entidad.ObjetoJsonEstado.TipoEstado == EnumConfig.GetDescription(TipoEstadoComponente.Ajuste))
                {
                    var count = await _componenteRepository.WhereWithCondition(x => x.UpaId == entidad.UpaId
                                                                            && x.ModuloComponenteId == entidad.ModuloComponenteId
                                                                            && x.DireccionRegistro == entidad.DireccionRegistro).CountAsync();
                    if (count >= 2)
                    {
                        throw new HttpStatusCodeException(HttpStatusCode.Conflict, $"Ya existe la dirección de registro {entidad.DireccionRegistro}");
                    }
                }
                else
                {
                    bool existeDireccionRegistro = await _componenteRepository.AnyWithCondition(x => x.UpaId == entidad.UpaId
                                                                            && x.NombreComponente == entidad.NombreComponente
                                                                            && x.DireccionRegistro == entidad.DireccionRegistro);
                    if (existeDireccionRegistro)
                    {
                        throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Ya existe la dirección de registro en la upa.");
                    }
                }
            }
        }

        public async Task<IEnumerable<NameDTO>> GetComponentsJustNamesById(UpaActivitiesFilterRequest upaActivitiesfilter, bool IsAdmin)
        {
            return await _componenteRepository.GetComponentesPorUpaModuloId(upaActivitiesfilter, IsAdmin);
        }

        public async Task<ResponseDTO> UpdateByAdmin(EComponenteLaboratorio entidad)
        {
            var componente = await _componenteRepository.GetById(entidad.Id);
            if (componente == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.");

            bool existeRepetido = await _componenteRepository.AnyWithCondition(x => x.ActividadId == componente.ActividadId && x.UpaId == componente.UpaId
                                                              && x.ModuloComponenteId == componente.ModuloComponenteId && x.NombreComponente
                                                              == entidad.NombreComponente && x.Id != entidad.Id);
            if (existeRepetido)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, $"Ya existe un componente con el mismo nombre {entidad.NombreComponente} de la upa y modulo escogido.");
            }
            componente.NombreComponente = entidad.NombreComponente;
            componente.ActividadId = entidad.ActividadId;
            componente.ModuloComponenteId = entidad.ModuloComponenteId;
            if (!string.IsNullOrWhiteSpace(entidad.JsonEstadoComponente))
            {
                componente.JsonEstadoComponente = entidad.JsonEstadoComponente;
            }
            await _componenteRepository.Update(componente);
            return Responses.SetOkMessageEditResponse(componente);
        }

        public async Task<List<int>> GetRegistrationAddressesByUpaModulo(UpaModuleActivityFilterRequest FilterRequest)
        {
            var direccionesRegistro = Enumerable.Range(0, 256).ToList();
            bool existeUpa = await _upaRepository.AnyWithCondition(x => x.Id == FilterRequest.UpaId);

            if (!existeUpa)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la upa.");
            }
            bool existeModulo = await _moduloRepository.AnyWithCondition(x => x.Id == FilterRequest.ModuloId);

            if (!existeModulo)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el modulo.");
            }
            bool existeActividad = await _actividadRepository.AnyWithCondition(x => x.Id == FilterRequest.ActividadId);

            if (!existeActividad)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la actividad.");
            }
            return await _componenteRepository.GetDireccionesRegistroDisponibles(FilterRequest, direccionesRegistro);
        }
    }
}



