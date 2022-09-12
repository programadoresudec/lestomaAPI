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
    public class ModuloService : IModuloService
    {
        private readonly Response _respuesta = new();
        private readonly ModuloRepository _moduloRepository;
        public ModuloService(ModuloRepository moduloRepository)
        {
            _moduloRepository = moduloRepository;
        }

        public async Task<IEnumerable<EModuloComponente>> GetAll()
        {
            var listado = await _moduloRepository.GetAll();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public IQueryable<EModuloComponente> GetAllForPagination()
        {
            var listado = _moduloRepository.GetAllAsQueryable();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public async Task<Response> GetById(Guid id)
        {
            var query = await _moduloRepository.GetById(id);
            if (query != null)
            {
                _respuesta.Data = query;
                _respuesta.StatusCode = (int)HttpStatusCode.OK;
                _respuesta.IsExito = true;
                _respuesta.Mensaje = "Encontrado";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el modulo requerido.");
            }
            return _respuesta;
        }
        public async Task<Response> Create(EModuloComponente entidad)
        {
            bool existe = await _moduloRepository.ExisteModulo(entidad.Nombre, Guid.Empty);
            if (!existe)
            {
                await _moduloRepository.Create(entidad);
                _respuesta.IsExito = true;
                _respuesta.Data = entidad;
                _respuesta.StatusCode = (int)HttpStatusCode.Created;
                _respuesta.Mensaje = "se ha creado satisfactoriamente.";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya esta en uso.");
            }
            return _respuesta;
        }
        public async Task<Response> Update(EModuloComponente entidad)
        {
            var response = await GetById(entidad.Id);
            var Modulo = (EModuloComponente)response.Data;
            bool existe = await _moduloRepository.ExisteModulo(entidad.Nombre, Modulo.Id, true);
            if (!existe)
            {
                Modulo.Nombre = entidad.Nombre;
                await _moduloRepository.Update(Modulo);
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
        public async Task Delete(Guid id)
        {
            var entidad = await GetById(id);
            await _moduloRepository.Delete((EModuloComponente)entidad.Data);
        }
    }
}