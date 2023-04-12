﻿using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.DTOs.Sync;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface ILaboratorioService
    {
        Task<ResponseDTO> CreateDetail(ELaboratorio detalle);
        Task<ResponseDTO> BulkSyncDataOffline(IEnumerable<ELaboratorio> datosOffline);
        Task SendEmailFinishMerge(string email);
        Task<IEnumerable<DataOnlineSyncDTO>> GetDataOfUserToSyncDeviceDatabase(UpaActivitiesFilterRequest filtro, bool isSuperAdmin);
        Task<IEnumerable<NameDTO>> GetModulesByUpaActivitiesUserId(UpaActivitiesFilterRequest filtro);
        Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByUpaAndModuleId(UpaModuleFilterRequest filtro);
        Task<ResponseDTO> GetComponentRecentTrama(Guid id);
    }
}
