using lestoma.CommonUtils.DTOs;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LSUpasActividades : IGenericCRUD<EUpaActividad>
    
    {
        private readonly Response _respuesta = new();

        private readonly DAOUpaActividad _upasActividadesRepository;
        public LSUpasActividades(DAOUpaActividad upasActividadesRepository)
        {
            _upasActividadesRepository = upasActividadesRepository;
        }

        public Task<Response> ActualizarAsync(EUpaActividad entidad)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response> CrearAsync(EUpaActividad entidad)
        {
            return await _upasActividadesRepository.CrearVarios(entidad);
        }

        public Task EliminarAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<EUpaActividad>> GetAll()
        {
           var query =  await _upasActividadesRepository.GetDetalleUpaActividad();

            return query;
        }

        public Task<Response> GetByIdAsync(object id)
        {
            throw new System.NotImplementedException();
        }
    }
}
