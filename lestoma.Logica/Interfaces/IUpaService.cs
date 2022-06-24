using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;

namespace lestoma.Logica.Interfaces
{
    public interface IUpaService : IGenericCRUD<EUpa, Guid>
    {
        List<NameDTO> GetUpasJustNames();
    }
}
