﻿using lestoma.CommonUtils.DTOs;
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
    public class DetalleUpaActividadService : IDetalleUpaActividadService

    {
        private readonly Response _respuesta = new();

        private readonly UpaActividadRepository _upasActividadesRepository;
        public DetalleUpaActividadService(UpaActividadRepository upasActividadesRepository)
        {
            _upasActividadesRepository = upasActividadesRepository;
        }

        public async Task<Response> UpdateInCascade(EUpaActividad entidad)
        {
            await _upasActividadesRepository.CreateRelation(entidad);
            return new Response
            {
                IsExito = true,
                Mensaje = "Se ha creado satisfactoriamente.",
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> CreateInCascade(EUpaActividad entidad)
        {

            await _upasActividadesRepository.CreateRelation(entidad);
            return new Response
            {
                IsExito = true,
                Mensaje = "Se ha creado satisfactoriamente.",
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<IEnumerable<string>> GetActivities(int UserId, Guid upaId)
        {
            return await _upasActividadesRepository.GetActivities(UserId, upaId);
        }

        public async Task<IEnumerable<EUpaActividad>> GetAll()
        {
            return await _upasActividadesRepository.GetAll();
        }

        public IQueryable<DetalleUpaActividadDTO> GetAllForPagination()
        {
            var query = _upasActividadesRepository.GetAllRelation();
            if (!query.Any())
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay detalles.");
            return query;
        }

        public async Task<Guid> GetUpaByUserId(int UserId)
        {
            return await _upasActividadesRepository.GetUpasByUserId(UserId);
        }

        public async Task<List<NameDTO>> GetActivitiesByUpaUserId(UpaUserFilterRequest filtro)
        {
            return await _upasActividadesRepository.GetActivitiesByUpaUserId(filtro);
        }


    }
}
