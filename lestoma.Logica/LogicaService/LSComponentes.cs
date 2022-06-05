using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
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
    public class LSComponentes : IComponenteService
    {
        private DAOComponente _componentR;
        private readonly Response _respuesta = new();

        public LSComponentes(DAOComponente componente)
        {

            _componentR = componente;

        }
        public async Task<IEnumerable<EComponentesLaboratorio>> GetAllAsync()
        {
            var listado = await _componentR.GetAll();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido");
            }
            return listado;
        }
        public async Task<Response> CrearAsync(EComponentesLaboratorio entidad)
        {
            entidad.Id = Guid.NewGuid();
            await _componentR.Create(entidad);
            _respuesta.IsExito = true;
            _respuesta.Data = entidad;
            _respuesta.StatusCode = (int)HttpStatusCode.Created;
            _respuesta.Mensaje = "Se ha creado";
            return _respuesta;
        }
        public List<NameDTO> GetComponentesJustNames()
        {
            return _componentR.GetComponentesJustNames();

        }

        public async Task<Response> GetByIdAsync(Guid id)
        {
            var query = await _componentR.GetById(id);
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

        public async Task<Response> ActualizarAsync(EComponentesLaboratorio entidad)
        {
            var response = await GetByIdAsync(entidad.Id);
            var comp = (EComponentesLaboratorio)response.Data;
            comp.Nombre = entidad.Nombre;
            await _componentR.Update(comp);
            _respuesta.IsExito = true;
            _respuesta.StatusCode = (int)HttpStatusCode.OK;
            _respuesta.Mensaje = "se ha editado satisfactoriamente.";
            return _respuesta;
        }

        public async Task EliminarAsync(Guid id)
        {
            var entidad = await GetByIdAsync(id);
            await _componentR.Delete((EComponentesLaboratorio)entidad.Data);
        }



        public IQueryable<EComponentesLaboratorio> GetAllAsQueryable()
        {
            var listado = _componentR.GetAllAsQueryable();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }
    }


}



