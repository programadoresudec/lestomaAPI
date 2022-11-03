﻿using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using Microsoft.EntityFrameworkCore;
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
            ReporteDTO reporteDTO = new ReporteDTO();
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
                   SetPointOut = GetSetPointOut(x.ValorCalculadoTramaRecibida),
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

        private string GetSetPointOut(double? valorCalculadoTramaRecibida)
        {
            string valor;
            switch (valorCalculadoTramaRecibida)
            {
                case 0:
                    valor = "Actuador Apagado.";
                    break;
                case 1:
                    valor = "Actuador Encendido.";
                    break;
                case 200:
                    valor = HttpStatusCode.OK.ToString();
                    break;
                case 409:
                    valor = $"{HttpStatusCode.Conflict} en la trama.";
                    break;
                default:
                    valor = valorCalculadoTramaRecibida.ToString();
                    break;
            }
            return valor;
        }

        public async Task<ReporteDTO> ReportByDate(ReportFilterRequest reporte)
        {
            ReporteDTO reporteDTO = new ReporteDTO();
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
            ReporteDTO reporteDTO = new ReporteDTO();
            try
            {
                var query = _db.TablaDetalleLaboratorio.Include(componente => componente.ComponenteLaboratorio)
                        .ThenInclude(upa => upa.Upa).Include(componente => componente.ComponenteLaboratorio)
                        .ThenInclude(modulo => modulo.ModuloComponente).AsNoTracking();
                if (reporte.Filtro.UpaId != Guid.Empty)
                {
                    query = query.Where(x => x.ComponenteLaboratorio.UpaId == reporte.Filtro.UpaId);
                }
                if (reporte.ComponentesId.Count > 0)
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
    }
}
