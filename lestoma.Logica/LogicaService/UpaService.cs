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
    public class UpaService : IUpaService
    {
        private readonly ResponseDTO _respuesta = new();
        private readonly UpaRepository _upaRepository;
        public UpaService(UpaRepository upaRepository)
        {
            _upaRepository = upaRepository;
        }

        public async Task<IEnumerable<EUpa>> GetAll()
        {
            var listado = await _upaRepository.GetAll();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public IQueryable<EUpa> GetAllForPagination()
        {
            var listado = _upaRepository.GetAllAsQueryable();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public async Task<ResponseDTO> GetById(Guid id)
        {
            var query = await _upaRepository.GetById(id);
            if (query != null)
            {
                _respuesta.Data = query;
                _respuesta.IsExito = true;
                _respuesta.MensajeHttp = "Encontrado";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la upa.");
            }
            return _respuesta;
        }
        public async Task<ResponseDTO> Create(EUpa entidad)
        {
            bool existe = await _upaRepository.ExisteUpa(entidad.Nombre, entidad.Id);
            if (existe)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya esta en uso.");
            var superadmin = await _upaRepository.GetSuperAdmin();
            if (superadmin == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "no hay Super Administradores.");
            entidad.SuperAdminId = superadmin.Id;
            entidad.Id = Guid.NewGuid();
            await _upaRepository.Create(entidad);
            _respuesta.IsExito = true;
            _respuesta.Data = entidad;
            _respuesta.StatusCode = (int)HttpStatusCode.Created;
            _respuesta.MensajeHttp = "se ha creado satisfactoriamente.";
            return _respuesta;
        }
        public async Task<ResponseDTO> Update(EUpa entidad)
        {
            var response = await GetById(entidad.Id);
            var upa = (EUpa)response.Data;
            bool existe = await _upaRepository.ExisteUpa(entidad.Nombre, upa.Id, true);
            if (!existe)
            {
                upa.Nombre = entidad.Nombre;
                upa.CantidadActividades = entidad.CantidadActividades;
                upa.Descripcion = entidad.Descripcion;
                await _upaRepository.Update(upa);
                _respuesta.IsExito = true;
                _respuesta.StatusCode = (int)HttpStatusCode.OK;
                _respuesta.MensajeHttp = "se ha editado satisfactoriamente.";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya está en uso.");
            }

            return _respuesta;
        }
        public async Task Delete(Guid id)
        {
            var entidad = await GetById(id);
            await _upaRepository.Delete((EUpa)entidad.Data);
        }

        public List<NameDTO> GetUpasJustNames()
        {
            return _upaRepository.GetUpasJustNames();
        }
    }
}
