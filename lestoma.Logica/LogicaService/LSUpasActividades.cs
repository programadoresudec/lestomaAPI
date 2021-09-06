using lestoma.CommonUtils.DTOs;
using lestoma.Data.DAO;
using lestoma.Logica.Interfaces;

namespace lestoma.Logica.LogicaService
{
    public class LSUpasActividades : IUpasActividadesService
    {
        private readonly Response _respuesta = new();

        private readonly DAOUpaActividad _upasActividadesRepository;
        public LSUpasActividades(DAOUpaActividad upasActividadesRepository)
        {
            _upasActividadesRepository = upasActividadesRepository;
        }


    }
}
