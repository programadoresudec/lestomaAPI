using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;

namespace lestoma.Logica.Interfaces
{
    public interface IActividadService : IGenericCRUD<EActividad, Guid>
    {
        List<NameDTO> GetActividadesJustNames();
    }
}
