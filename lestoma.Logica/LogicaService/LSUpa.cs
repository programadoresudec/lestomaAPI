﻿using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LSUpa : IUpaService
    {
        private readonly Response _respuesta = new();
        private readonly DAOUpa _upaRepository;
        public LSUpa(DAOUpa upaRepository)
        {
            _upaRepository = upaRepository;
        }

        public async Task<Response> GetByIdAsync(object id)
        {
            var query = await _upaRepository.GetById(id);
            if (query != null)
            {
                _respuesta.Data = query;
                _respuesta.IsExito = true;
                _respuesta.Mensaje = "Encontrado";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la upa.");
            }
            return _respuesta;
        }
        public async Task<List<EUpa>> GetAll()
        {
            var listado = await _upaRepository.GetAll();
            if (listado.ToList().Count == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado.ToList();
        }

        public async Task<Response> ActualizarAsync(EUpa entidad)
        {
            var response = await GetByIdAsync(entidad.Id);
            var upa = (EUpa)response.Data;
            bool existe = await _upaRepository.ExisteUpa(entidad.Nombre, true, upa.Id);
            if (!existe)
            {
                upa.Nombre = entidad.Nombre;
                upa.CantidadActividades = entidad.CantidadActividades;
                upa.Descripcion = entidad.Descripcion;
                await _upaRepository.Update(upa);
                _respuesta.IsExito = true;
                _respuesta.StatusCode = (int)HttpStatusCode.OK;
                _respuesta.Mensaje = "se ha editado satisfactoriamente.";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya está en uso.");
            }

            return _respuesta;
        }

        public async Task<Response> CrearAsync(EUpa entidad)
        {
            bool existe = await _upaRepository.ExisteUpa(entidad.Nombre);
            if (!existe)
            {
                var superadmin = await _upaRepository.GetSuperAdmin();
                if (superadmin != null)
                {
                    entidad.SuperAdminId = superadmin.Id;

                    await _upaRepository.Create(entidad);
                    _respuesta.IsExito = true;
                    _respuesta.Data = entidad;
                    _respuesta.StatusCode = (int)HttpStatusCode.Created;
                    _respuesta.Mensaje = "se ha creado satisfactoriamente.";
                }
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya esta en uso.");
            }
            return _respuesta;
        }

        public async Task EliminarAsync(object id)
        {
            var entidad = await GetByIdAsync(id);
            await _upaRepository.Delete((EUpa)entidad.Data);
        }

        public List<NameDTO> GetUpasJustNames()
        {
            return _upaRepository.GetUpasJustNames();
        }
    }
}
