using lestoma.CommonUtils.DTOs;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System.Collections.Generic;
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

        public async Task<List<EUpaActividad>> GetAll()
        {
            var query = await _upasActividadesRepository.GetDetalleUpaActividad();

            return query;
        }

       
    }
}
