using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Listados;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
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
    public class ComponenteService : IComponenteService
    {
        private ComponenteRepository _componenteRepository;
        private ActividadRepository _actividadRepo;
        private UpaRepository _upaRepo;

        public ComponenteService(ComponenteRepository componente, ActividadRepository _actividadRepository,
            UpaRepository upaRepository)
        {
            _componenteRepository = componente;
            _actividadRepo = _actividadRepository;
            _upaRepo = upaRepository;
        }
        public async Task<IEnumerable<EComponenteLaboratorio>> GetAll()
        {
            var listado = await _componenteRepository.GetAll();
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
            if (entidad.ObjetoJsonEstado.TipoEstado == EnumConfig.GetDescription(TipoEstadoComponente.Lectura))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonAjuste = JsonSerializer.Serialize(new ListadoEstadoComponente().GetEstadoAjuste(), options);

                EComponenteLaboratorio entidadAjuste = new EComponenteLaboratorio
                {
                    ActividadId = entidad.ActividadId,
                    DireccionRegistro = entidad.DireccionRegistro,
                    Id = Guid.NewGuid(),
                    UpaId = entidad.UpaId,
                    NombreComponente = $"SP {entidad.NombreComponente}",
                    JsonEstadoComponente = jsonAjuste,
                    ModuloComponenteId = entidad.ModuloComponenteId
                };
                await _componenteRepository.CreateMultiple(new List<EComponenteLaboratorio> { entidad, entidadAjuste });
            }
            else
            {
                await _componenteRepository.Create(entidad);
            }
            return Responses.SetCreatedResponse(entidad);
        }

        public async Task<IEnumerable<NameDTO>> GetComponentsJustNames()
        {
            return await _componenteRepository.GetComponentesJustNames();
        }

        public async Task<ResponseDTO> GetById(Guid id)
        {
            var query = await _componenteRepository.GetInfoById(id);
            if (query == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el componente.");
            return Responses.SetOkResponse(query);
        }

        public async Task<ResponseDTO> Update(EComponenteLaboratorio entidad)
        {
            var componente = await _componenteRepository.GetById(entidad.Id);
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
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
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
            var existeDireccionRegistro = await _componenteRepository.AnyWithCondition(x => x.ActividadId == entidad.ActividadId && x.UpaId == entidad.UpaId
                                                                      && x.ModuloComponenteId == entidad.ModuloComponenteId && x.NombreComponente == entidad.NombreComponente
                                                                      && x.DireccionRegistro == entidad.DireccionRegistro);
            if (existeDireccionRegistro)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Ya se encuentra registrado un componente con la misma dirección de registro en la upa.");
            }

            if (IsCreated)
            {
                var existeRepetido = await _componenteRepository.AnyWithCondition(x => x.ActividadId == entidad.ActividadId && x.UpaId == entidad.UpaId
                                                                      && x.ModuloComponenteId == entidad.ModuloComponenteId && x.NombreComponente == entidad.NombreComponente);
                if (existeRepetido)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.Conflict, "Ya se encuentra registrado un componente con los mismos parametros.");
                }
            }
        }

        public async Task<IEnumerable<NameDTO>> GetComponentsJustNamesById(UpaActivitiesFilterRequest upaActivitiesfilter, bool IsAdmin)
        {
            return await _componenteRepository.GetComponentesPorUpaId(upaActivitiesfilter, IsAdmin);
        }
    }


}



