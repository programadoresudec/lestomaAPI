using lestoma.CommonUtils.DTOs;
using lestoma.Data;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using lestoma.Logica.MyException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LSActividad : IActividadService
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

        public async Task<Response> CrearActividad(EActividad actividad)
        {
            bool existe = await _actividadRepository.ExisteActividad(actividad.Nombre);
            if (!existe)
            {
                await _actividadRepository.Create(actividad);
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

        public async Task<Response> EditarActividad(EActividad actividad)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EActividad>> ListaActividades()
        {
            var query = await _actividadRepository.GetAll();
            if (query.ToList().Count == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay actividades.");
            }
            return query.ToList();
        }
    }
}
