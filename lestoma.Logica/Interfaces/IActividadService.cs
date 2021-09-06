using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IActividadService
    {

        Task<Response> CrearActividad(EActividad actividad);
        Task<Response> EditarActividad(EActividad actividad);
        Task<List<EActividad>> ListaActividades();
    }
}
