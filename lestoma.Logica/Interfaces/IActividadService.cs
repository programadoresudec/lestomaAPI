using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IActividadService : IGenericCRUD<EActividad, Guid>
    {
        Task<IEnumerable<NameDTO>> GetActividadesJustNames();
    }
}
