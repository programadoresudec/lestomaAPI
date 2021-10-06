﻿using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.Data;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LSActividad : IGenericCRUD<EActividad>
    {
        private readonly Response _respuesta = new();

        private readonly DAOActividad _actividadRepository;
        public LSActividad(DAOActividad actividadRepository)
        {
            _actividadRepository = actividadRepository;
        }
        public LSActividad(string dbPath)
        {
            Mapeo _db = new Mapeo(dbPath);
            _actividadRepository = new DAOActividad(_db);
        }

        public Task<Response> ActualizarAsync(EActividad entidad)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> CrearAsync(EActividad entidad)
        {
            bool existe = await _actividadRepository.ExisteActividad(entidad.Nombre);
            if (!existe)
            {
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

        public async Task EliminarAsync(object id)
        {
            var entidad = await GetByIdAsync(id);
            await _actividadRepository.Delete((EActividad)entidad.Data);
        }

        public async Task<List<EActividad>> GetAll()
        {
            var query = await _actividadRepository.GetAll();
            if (query.ToList().Count == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay actividades.");
            }
            return query.ToList();
        }

        public async Task<Response> GetByIdAsync(object id)
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
    }
}
