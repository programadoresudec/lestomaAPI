using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface ILaboratorioService
    {
        Task<ResponseDTO> CreateDetail(ELaboratorio detalle);
        Task<ResponseDTO> BulkSyncDataOffline(IEnumerable<ELaboratorio> datosOffline);
        Task SendEmailFinishMerge(string email);
        Task<IEnumerable<DataComponentSyncDTO>> GetDataOfUserToSyncDeviceDatabase(Guid upaId);
        Task<IEnumerable<NameDTO>> GetModulesByUpaActivitiesUserId(UpaActivitiesFilterRequest filtro);
        Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByModuleId(Guid id);
    }
}
