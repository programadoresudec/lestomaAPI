using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.Data.Repositories;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
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

        public async Task<IEnumerable<EModuloComponente>> GetAllAsync()
        {
            var listado = await _moduloRepository.GetAll();
            if (listado.Count() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public IQueryable<EModuloComponente> GetAllAsQueryable()
        {
            var listado = _moduloRepository.GetAllAsQueryable();
            if (listado.Count() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public async Task<Response> GetByIdAsync(int id)
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
        public async Task<Response> CrearAsync(EModuloComponente entidad)
        {
            bool existe = await _moduloRepository.ExisteModulo(entidad.Nombre, entidad.Id);
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
        public async Task<Response> ActualizarAsync(EModuloComponente entidad)
        {
            var response = await GetByIdAsync(entidad.Id);
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
        public async Task EliminarAsync(int id)
        {
            var entidad = await GetByIdAsync(id);
            await _moduloRepository.Delete((EModuloComponente)entidad.Data);
        }
    }
}