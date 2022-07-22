using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class ReporteRepository
    {
        private readonly LestomaContext _db;
        public ReporteRepository(LestomaContext context)
        {
            _db = context;
        }

        public async Task<List<string>> GetCorreosRolSuperAdmin()
        {
            return await _db.TablaUsuarios.Where(x => x.EstadoId == (int)TipoRol.SuperAdministrador)
                .Select(x => x.Email).ToListAsync();
        }

        public async Task<ReporteDTO> DailyReport(FilterDateRequest filtro)
        {
            ReporteDTO reporteDTO = new ReporteDTO();

            return reporteDTO;
        }

        public async Task<ReporteDTO> ReportByDate(FilterReportRequest reporte)
        {
            ReporteDTO reporteDTO = new ReporteDTO();
            return reporteDTO;
        }

        public async Task<ReporteDTO> ReportByComponents(FilterReportComponentRequest reporte)
        {
            ReporteDTO reporteDTO = new ReporteDTO();


            return reporteDTO;
        }
    }
}
