using lestoma.CommonUtils.DTOs;
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
        Task<Response> CreateDetail(ELaboratorio detalle);
        Task<Response> SyncLabDataOffline(IEnumerable<ELaboratorio> datosOffline);
        Task SendEmailFinishMerge(string email);
        Task<IEnumerable<DataComponentSyncDTO>> GetDataBySyncToMobileByUpaId(Guid upaId);
    }
}
