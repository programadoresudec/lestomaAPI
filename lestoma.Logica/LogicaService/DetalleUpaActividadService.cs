using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
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
    public class DetalleUpaActividadService : IDetalleUpaActividadService
    {
        private readonly UpaActividadRepository _upasActividadesRepository;
        public DetalleUpaActividadService(UpaActividadRepository upasActividadesRepository)
        {
            _upasActividadesRepository = upasActividadesRepository;
        }

        public async Task<ResponseDTO> UpdateInCascade(EUpaActividad entidad)
        {
            await _upasActividadesRepository.UpdateRelation(entidad);
            return Responses.SetOkMessageEditResponse(entidad);
        }

        public async Task<ResponseDTO> CreateInCascade(EUpaActividad entidad)
        {
            bool tieneUpa = await _upasActividadesRepository.AnyWithCondition(x => x.UsuarioId == entidad.UsuarioId);
            if (tieneUpa)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El usuario ya tiene asignada una upa.");
            await _upasActividadesRepository.CreateRelation(entidad);
            return Responses.SetCreatedResponse(entidad);
        }

        public async Task<IEnumerable<EUpaActividad>> GetAll()
        {
            return await _upasActividadesRepository.GetAll();
        }

        public IQueryable<DetalleUpaActividadDTO> GetAllForPagination()
        {
            var query = _upasActividadesRepository.GetAllRelation();
            return query;
        }

        public async Task<Guid> GetUpaByUserId(int UserId)
        {
            return await _upasActividadesRepository.GetUpaByUserId(UserId);
        }

        public async Task<IEnumerable<NameDTO>> GetActivitiesByUpaUserId(UpaUserFilterRequest filtro)
        {
            return await _upasActividadesRepository.GetActivitiesByUpaUserId(filtro);
        }

        public async Task<IEnumerable<NameDTO>> GetActivitiesByUpaId(Guid idUpa)
        {
            return await _upasActividadesRepository.GetActivitiesByUpaId(idUpa);
        }
    }
}
