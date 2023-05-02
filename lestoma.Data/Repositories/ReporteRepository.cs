using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<bool> ExistCorreo(string email)
        {
            return await _db.TablaUsuarios.AnyAsync(x => x.Email.Equals(email));
        }

        public async Task<List<string>> GetCorreosRolSuperAdmin()
        {
            return await _db.TablaUsuarios.Where(x => x.RolId == (int)TipoRol.SuperAdministrador
                    && x.EstadoId == (int)TipoEstadoUsuario.Activado).Select(x => x.Email).ToListAsync();
        }

        public async Task<ReporteDTO> DailyReport(DateFilterRequest filtro)
        {
            ReporteDTO reporteDTO = new();
            try
            {
                var query = await _db.TablaDetalleLaboratorio.Include(componente => componente.ComponenteLaboratorio)
               .ThenInclude(upa => upa.Upa).Include(componente => componente.ComponenteLaboratorio)
               .ThenInclude(modulo => modulo.ModuloComponente)
               .Where(x => x.FechaCreacionDispositivo >= filtro.FechaInicial && x.FechaCreacionDispositivo <= filtro.FechaFinal)
               .Select(x => new ReportInfo
               {
                   Usuario = x.Session,
                   Componente = x.ComponenteLaboratorio.NombreComponente,
                   SetPointIn = x.ValorCalculadoTramaEnviada == null ? "N/A" : x.ValorCalculadoTramaEnviada.ToString(),
                   ResultSetPointOut = x.ValorCalculadoTramaRecibida,
                   Modulo = x.ComponenteLaboratorio.ModuloComponente.Nombre,
                   FechaDispositivo = x.FechaCreacionDispositivo,
                   FechaServidor = x.FechaCreacionServer,
                   NombreUpa = x.ComponenteLaboratorio.Upa.Nombre,
                   Estado = x.ComponenteLaboratorio.ObjetoJsonEstado.TipoEstado
               }).ToListAsync();
                reporteDTO.Reporte = query;
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, @$"Error: {ex.Message}");
            }
            return reporteDTO;
        }



        public async Task<ReporteDTO> ReportByDate(ReportFilterRequest reporte)
        {
            ReporteDTO reporteDTO = new();
            try
            {
                var query = _db.TablaDetalleLaboratorio.Include(componente => componente.ComponenteLaboratorio)
                 .ThenInclude(upa => upa.Upa).Include(componente => componente.ComponenteLaboratorio)
                 .ThenInclude(modulo => modulo.ModuloComponente).AsNoTracking();
                if (reporte.UpaId != Guid.Empty)
                {
                    query = query.Where(x => x.ComponenteLaboratorio.UpaId == reporte.UpaId);
                }
                query = query.Where(x => x.FechaCreacionDispositivo >= reporte.FechaInicial && x.FechaCreacionDispositivo <= reporte.FechaFinal);
                reporteDTO.Reporte = await query.Select(x => new ReportInfo
                {
                    Usuario = x.Session,
                    Componente = x.ComponenteLaboratorio.NombreComponente,
                    SetPointIn = x.ValorCalculadoTramaEnviada == null ? "N/A" : x.ValorCalculadoTramaEnviada.ToString(),
                    ResultSetPointOut = x.ValorCalculadoTramaRecibida,
                    Modulo = x.ComponenteLaboratorio.ModuloComponente.Nombre,
                    FechaDispositivo = x.FechaCreacionDispositivo,
                    FechaServidor = x.FechaCreacionServer,
                    NombreUpa = x.ComponenteLaboratorio.Upa.Nombre,
                    Estado = x.ComponenteLaboratorio.ObjetoJsonEstado.TipoEstado
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, @$"Error: {ex.Message}");
            }
            return reporteDTO;
        }

        public async Task<ReporteDTO> ReportByComponents(ReportComponentFilterRequest reporte)
        {
            ReporteDTO reporteDTO = new();
            try
            {
                var query = _db.TablaDetalleLaboratorio.Include(componente => componente.ComponenteLaboratorio)
                        .ThenInclude(upa => upa.Upa).Include(componente => componente.ComponenteLaboratorio)
                        .ThenInclude(modulo => modulo.ModuloComponente).AsNoTracking();
                if (reporte.Filtro.UpaId != Guid.Empty)
                {
                    query = query.Where(x => x.ComponenteLaboratorio.UpaId == reporte.Filtro.UpaId);
                }
                var idTodos = reporte.ComponentesId.FirstOrDefault(x => x.Equals(Guid.Empty));
                if (reporte.ComponentesId.Count > 0 && !reporte.ComponentesId.Contains(idTodos))
                {
                    query = query.Where(x => reporte.ComponentesId.Contains(x.ComponenteLaboratorioId));
                }

                query = query.Where(x => x.FechaCreacionDispositivo >= reporte.Filtro.FechaInicial
                && x.FechaCreacionDispositivo <= reporte.Filtro.FechaFinal);
                reporteDTO.Reporte = await query.Select(x => new ReportInfo
                {
                    Usuario = x.Session,
                    Componente = x.ComponenteLaboratorio.NombreComponente,
                    SetPointIn = x.ValorCalculadoTramaEnviada == null ? "N/A" : x.ValorCalculadoTramaEnviada.ToString(),
                    ResultSetPointOut = x.ValorCalculadoTramaRecibida,
                    Modulo = x.ComponenteLaboratorio.ModuloComponente.Nombre,
                    FechaDispositivo = x.FechaCreacionDispositivo,
                    FechaServidor = x.FechaCreacionServer,
                    NombreUpa = x.ComponenteLaboratorio.Upa.Nombre,
                    Estado = x.ComponenteLaboratorio.ObjetoJsonEstado.TipoEstado
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, @$"Error: {ex.Message}");
            }
            return reporteDTO;
        }

        public async Task<TimeSpan?> GetDailyReportTime(string kEY_REPORT_DAILY)
        {
            await using (NpgsqlConnection connection = new NpgsqlConnection(_db.Database.GetConnectionString()))
            {
                connection.Open();
                var sql = "SELECT hg.score as fecha FROM hangfire_lestoma.set hg WHERE value = @key";

                await using NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@key", kEY_REPORT_DAILY);
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    reader.Read();
                    double value = (double)reader["fecha"];
                    DateTime date = UnixTimeStampToDateTime(value);
                    return new TimeSpan(date.Hour, date.Minute, date.Second);
                }
                return null;
            }
        }
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
