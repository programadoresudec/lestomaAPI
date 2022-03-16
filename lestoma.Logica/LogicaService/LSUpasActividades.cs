using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LSUpasActividades : IDetalleUpaActividadService

    {
        private readonly Response _respuesta = new();

        private readonly DAOUpaActividad _upasActividadesRepository;
        public LSUpasActividades(DAOUpaActividad upasActividadesRepository)
        {
            _upasActividadesRepository = upasActividadesRepository;
        }

        public Task<Response> ActualizarEnCascada(EUpaActividad entidad)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response> CrearEnCascada(EUpaActividad entidad)
        {
            return await _upasActividadesRepository.CrearVarios(entidad);
        }

        public Task EliminarEnCascada(int IdUsuario)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<EUpaActividad>> GetAll()
        {
           return  await _upasActividadesRepository.GetAll();
        }

        public IQueryable<EUpaActividad> GetAllAsQueryable()
        {
            var query = _upasActividadesRepository.GetDetalleUpaActividad();
            int variable = query.Count();
            if (variable == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay actividades.");
            }
            return query;
        }
    }
}
