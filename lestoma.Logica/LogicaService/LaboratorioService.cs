using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LaboratorioService : ILaboratorioService
    {
        public async Task<Response> CreateDetail(ELaboratorio detalle)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> MergeDetails(IEnumerable<ELaboratorio> datosOffline)
        {
            throw new NotImplementedException();
        }
    }
}
