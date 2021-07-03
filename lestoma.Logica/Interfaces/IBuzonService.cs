using lestoma.CommonUtils.Entities;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IBuzonService
    {
        Task<List<EBuzon>> Listado();
        Task<Response> AgregarReporte(BuzonCreacionRequest buzonCreacion);
        Task<EBuzon> GetBuzonById(int id);
    }
}
