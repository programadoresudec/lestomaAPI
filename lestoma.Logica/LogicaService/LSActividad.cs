using lestoma.CommonUtils.DTOs;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Response> CrearActividad(EActividad actividad)
        {
            bool existe = await _actividadRepository.ExisteActividad(actividad.Nombre);
            if (!existe)
            {
                await _actividadRepository.Create(actividad);
                _respuesta.IsExito = true;
                _respuesta.Mensaje = "se ha creado satisfactoriamente.";
            }
            else
            {
                _respuesta.Mensaje = "El nombre ya esta en uso utilice otro.";
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
            return query.ToList();
        }
    }
}
