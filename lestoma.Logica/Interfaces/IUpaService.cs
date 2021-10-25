using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IUpaService : IGenericCRUD<EUpa>
    {
        List<NameDTO> GetUpasJustNames();

    }
}
