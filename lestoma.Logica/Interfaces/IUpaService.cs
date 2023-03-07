﻿using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IUpaService : IGenericCRUD<EUpa, Guid>
    {
        Task<IEnumerable<NameDTO>> GetUpasJustNames();
        Task<ResponseDTO> UpdateProtocol(EProtocoloCOM protocolo);
        Task<short> GetSuperAdminId(int userId);
    }
}
