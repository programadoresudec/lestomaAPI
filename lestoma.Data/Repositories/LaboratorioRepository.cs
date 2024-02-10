using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.DTOs.Sync;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class LaboratorioRepository : GenericRepository<ELaboratorio>
    {
        private readonly LestomaContext _db;
        public LaboratorioRepository(LestomaContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<DataOnlineSyncDTO>> GetDataBySyncToMobileByUpaId(UpaActivitiesFilterRequest filtro, bool isSuperAdmin, bool isAuxiliar)
        {
            if (isAuxiliar)
                return await GetDataOfflineAuxiliar(filtro);

            else if (isSuperAdmin)
                return await GetDataOfflineSuperAdmin();

            else
                return await GetDataOfflineAdmin(filtro);
        }

        private async Task<IEnumerable<DataOnlineSyncDTO>> GetDataOfflineAuxiliar(UpaActivitiesFilterRequest filtro)
        {
            var parameters = new string[filtro.ActividadesId.Count()];
            var sqlParameters = new List<NpgsqlParameter>();
            for (var i = 0; i < filtro.ActividadesId.Count(); i++)
            {
                parameters[i] = string.Format("@p{0}", i);
                sqlParameters.Add(new NpgsqlParameter(parameters[i], filtro.ActividadesId.ToList()[i]));
            }

            var upaId = new NpgsqlParameter("upaId", filtro.UpaId);
            var estadoComponente = new NpgsqlParameter("estadoComponente", EnumConfig.GetDescription(TipoEstadoComponente.Ajuste));
            sqlParameters.Add(upaId);
            sqlParameters.Add(estadoComponente);

            string consulta = $@"SELECT  comp.*
                                         FROM laboratorio_lestoma.componente_laboratorio comp
                                               INNER JOIN laboratorio_lestoma.modulo_componente modulo ON comp.modulo_componente_id = modulo.id
                                         WHERE comp.actividad_id IN ({string.Join(", ", parameters)})
                                               AND comp.descripcion_estado::JSONB->>'TipoEstado' <> @estadoComponente 
                                               AND comp.upa_id = @upaId";

            var data = _db.TablaComponentesLaboratorio.FromSqlRaw(consulta, sqlParameters.ToArray())
                .Include(m => m.ModuloComponente).Include(a => a.Actividad).Include(u => u.Upa);
            return await data.Select(x => new DataOnlineSyncDTO
            {
                Id = x.Id,
                NombreComponente = x.NombreComponente,
                DescripcionEstadoJson = x.JsonEstadoComponente,
                Actividad = new NameDTO
                {
                    Id = x.Actividad.Id,
                    Nombre = x.Actividad.Nombre
                },
                DireccionRegistro = x.DireccionRegistro,
                Modulo = new NameDTO
                {
                    Id = x.ModuloComponente.Id,
                    Nombre = x.ModuloComponente.Nombre,
                },
                Upa = new NameDTO
                {
                    Id = x.Upa.Id,
                    Nombre = x.Upa.Nombre,
                },
                Protocolos = _db.TablaProtocoloCOM.Where(x => x.UpaId == filtro.UpaId)
                .Select(y => new ProtocoloSyncDTO
                {
                    Nombre = y.Nombre,
                    PrimerByteTrama = y.PrimerByteTrama
                }).ToList(),
            }).ToListAsync();
        }

        private async Task<IEnumerable<DataOnlineSyncDTO>> GetDataOfflineSuperAdmin()
        {
            return await (from componente in _db.TablaComponentesLaboratorio
                          join modulo in _db.TablaModuloComponentes on componente.ModuloComponenteId equals modulo.Id
                          join actividad in _db.TablaActividades on componente.ActividadId equals actividad.Id
                          join upa in _db.TablaUpas on componente.UpaId equals upa.Id
                          select new DataOnlineSyncDTO
                          {
                              Id = componente.Id,
                              NombreComponente = componente.NombreComponente,
                              DescripcionEstadoJson = componente.JsonEstadoComponente,
                              Actividad = new NameDTO
                              {
                                  Id = actividad.Id,
                                  Nombre = actividad.Nombre
                              },
                              DireccionRegistro = componente.DireccionRegistro,
                              Modulo = new NameDTO
                              {
                                  Id = modulo.Id,
                                  Nombre = modulo.Nombre,
                              },
                              Upa = new NameDTO
                              {
                                  Id = upa.Id,
                                  Nombre = upa.Nombre,
                              },
                              Protocolos = _db.TablaProtocoloCOM.Where(x => x.UpaId == upa.Id).Select(y => new ProtocoloSyncDTO
                              {
                                  Nombre = y.Nombre,
                                  PrimerByteTrama = y.PrimerByteTrama
                              }).ToList(),
                          }).ToListAsync();
        }

        private async Task<IEnumerable<DataOnlineSyncDTO>> GetDataOfflineAdmin(UpaActivitiesFilterRequest filtro)
        {
            return await (from componente in _db.TablaComponentesLaboratorio
                          join modulo in _db.TablaModuloComponentes on componente.ModuloComponenteId equals modulo.Id
                          join actividad in _db.TablaActividades on componente.ActividadId equals actividad.Id
                          join upa in _db.TablaUpas on componente.UpaId equals upa.Id
                          where componente.UpaId == filtro.UpaId && filtro.ActividadesId.Contains(componente.ActividadId)
                          select new DataOnlineSyncDTO
                          {
                              Id = componente.Id,
                              DescripcionEstadoJson = componente.JsonEstadoComponente,
                              Actividad = new NameDTO
                              {
                                  Id = actividad.Id,
                                  Nombre = actividad.Nombre
                              },
                              DireccionRegistro = componente.DireccionRegistro,
                              Modulo = new NameDTO
                              {
                                  Id = modulo.Id,
                                  Nombre = modulo.Nombre,
                              },
                              Upa = new NameDTO
                              {
                                  Id = upa.Id,
                                  Nombre = upa.Nombre,
                              },
                              NombreComponente = componente.NombreComponente,
                              Protocolos = _db.TablaProtocoloCOM.Where(x => x.UpaId == upa.Id).Select(y => new ProtocoloSyncDTO
                              {
                                  Nombre = y.Nombre,
                                  PrimerByteTrama = y.PrimerByteTrama
                              }).ToList(),
                          }).ToListAsync();
        }

        public async Task<IEnumerable<NameDTO>> GetModulesByUpaActivitiesUserId(UpaActivitiesFilterRequest filtro, bool IsAuxiliar)
        {
            if (IsAuxiliar)
            {
                var parameters = new string[filtro.ActividadesId.Count()];
                var sqlParameters = new List<NpgsqlParameter>();
                for (var i = 0; i < filtro.ActividadesId.Count(); i++)
                {
                    parameters[i] = string.Format("@p{0}", i);
                    sqlParameters.Add(new NpgsqlParameter(parameters[i], filtro.ActividadesId.ToList()[i]));
                }

                var upaId = new NpgsqlParameter("upaId", filtro.UpaId);
                var estadoComponente = new NpgsqlParameter("estadoComponente", EnumConfig.GetDescription(TipoEstadoComponente.Ajuste));
                sqlParameters.Add(upaId);
                sqlParameters.Add(estadoComponente);

                string consulta = $@"SELECT  modulo.id, modulo.nombre_modulo
                                         FROM laboratorio_lestoma.componente_laboratorio comp
                                               INNER JOIN laboratorio_lestoma.modulo_componente modulo ON comp.modulo_componente_id = modulo.id
                                         WHERE comp.actividad_id IN ({string.Join(", ", parameters)})
                                               AND comp.descripcion_estado::JSONB->>'TipoEstado' <> @estadoComponente 
                                               AND comp.upa_id = @upaId 
                                         GROUP BY modulo.id, modulo.nombre_modulo";

                var modulos = _db.TablaModuloComponentes.FromSqlRaw(consulta, sqlParameters.ToArray());
                return await modulos.Select(x => new NameDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                }).ToListAsync();
            }
            return await (from componente in _db.TablaComponentesLaboratorio
                          join modulo in _db.TablaModuloComponentes on componente.ModuloComponenteId equals modulo.Id
                          where componente.UpaId == filtro.UpaId && filtro.ActividadesId.Contains(componente.ActividadId)
                          group modulo by new { modulo.Id, modulo.Nombre } into grupo
                          select new NameDTO
                          {
                              Id = grupo.Key.Id,
                              Nombre = grupo.Key.Nombre
                          }).ToListAsync();
        }

        public async Task MergeDetails(IEnumerable<ELaboratorio> datosOffline)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {

                    await _db.AddRangeAsync(datosOffline);
                    await _db.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ObtenerException(ex, null);
                }
            }
        }

        public async Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByUpaAndModuleId(UpaModuleFilterRequest filtro)
        {
            var query = await (from componente in _db.TablaComponentesLaboratorio
                               join actividad in _db.TablaActividades on componente.ActividadId equals actividad.Id
                               where componente.ModuloComponenteId == filtro.ModuloId && componente.UpaId == filtro.UpaId
                               select new LaboratorioComponenteDTO
                               {
                                   Actividad = actividad.Nombre,
                                   Id = componente.Id,
                                   Nombre = componente.NombreComponente,
                                   DireccionRegistro = componente.DireccionRegistro,
                                   JsonEstado = componente.JsonEstadoComponente
                               }).ToListAsync();
            return query;
        }

        public async Task<TramaComponenteDTO> GetComponentRecentTrama(Guid id)
        {
            var componente = await _db.TablaDetalleLaboratorio.Where(x => x.ComponenteLaboratorioId == id).Include(y => y.ComponenteLaboratorio)
                 .OrderByDescending(d => d.FechaCreacionDispositivo)
                           .Select(x => new TramaComponenteDTO
                           {
                               TramaInPut = x.TramaEnviada,
                               TramaOutPut = x.TramaRecibida,
                               SetPointIn = x.ValorCalculadoTramaEnviada,
                               SetPointOut = x.ValorCalculadoTramaRecibida,
                               DireccionDeRegistro = x.ComponenteLaboratorio.DireccionRegistro,
                               FechaDispositivo = x.FechaCreacionDispositivo,
                               UpaId = x.ComponenteLaboratorio.UpaId
                           }).FirstOrDefaultAsync();

            var sqlParameters = new List<NpgsqlParameter>();
            var upaId = new NpgsqlParameter("upaId", componente.UpaId);
            var direccionRegistro = new NpgsqlParameter("direccionRegistro", componente.DireccionDeRegistro);
            var estadoComponente = new NpgsqlParameter("estadoComponente", EnumConfig.GetDescription(TipoEstadoComponente.Ajuste));
            sqlParameters.Add(upaId);
            sqlParameters.Add(direccionRegistro);
            sqlParameters.Add(estadoComponente);

            string consulta = $@"SELECT detalleLaboratorio.*
                                 FROM laboratorio_lestoma.detalle_laboratorio detalleLaboratorio
                                          INNER JOIN laboratorio_lestoma.componente_laboratorio comp
                                                     ON detalleLaboratorio.componente_laboratorio_id = comp.id
                                 WHERE comp.descripcion_estado::JSONB ->> 'TipoEstado' = @estadoComponente
                                   AND comp.direccion_registro = @direccionRegistro
                                   AND comp.upa_id = @upaId";

            var componentSetpoint = _db.TablaDetalleLaboratorio.FromSqlRaw(consulta, sqlParameters.ToArray()).Include(y => y.ComponenteLaboratorio);
            var dataComponentSetPoint = await componentSetpoint.Select(x => new TramaComponenteDTO
            {
                SetPointIn = x.ValorCalculadoTramaEnviada,
                SetPointOut = x.ValorCalculadoTramaEnviada,
                DireccionDeRegistro = x.ComponenteLaboratorio.DireccionRegistro,
                FechaDispositivo = x.FechaCreacionDispositivo
            }).OrderByDescending(y => y.FechaDispositivo).FirstOrDefaultAsync();
            if (dataComponentSetPoint != null && dataComponentSetPoint.FechaDispositivo > componente.FechaDispositivo)
            {
                return dataComponentSetPoint;
            }
            return componente;
        }

        public async Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByActivitiesOfUpaUserId(UpaActivitiesModuleFilterRequest filtro, bool IsAuxiliar)
        {
            if (!IsAuxiliar)
            {
                return await (from componente in _db.TablaComponentesLaboratorio
                              join actividad in _db.TablaActividades on componente.ActividadId equals actividad.Id
                              where componente.ModuloComponenteId == filtro.ModuloId && componente.UpaId == filtro.UpaId
                              && filtro.ActividadesId.Contains(componente.ActividadId)
                              select new LaboratorioComponenteDTO
                              {
                                  Actividad = actividad.Nombre,
                                  Id = componente.Id,
                                  Nombre = componente.NombreComponente,
                                  DireccionRegistro = componente.DireccionRegistro,
                                  JsonEstado = componente.JsonEstadoComponente
                              }).ToListAsync();
            }
            else
            {
                var parameters = new string[filtro.ActividadesId.Count()];
                var sqlParameters = new List<NpgsqlParameter>();
                for (var i = 0; i < filtro.ActividadesId.Count(); i++)
                {
                    parameters[i] = string.Format("@p{0}", i);
                    sqlParameters.Add(new NpgsqlParameter(parameters[i], filtro.ActividadesId.ToList()[i]));
                }

                var upaId = new NpgsqlParameter("upaId", filtro.UpaId);
                var moduloId = new NpgsqlParameter("moduloId", filtro.ModuloId);
                var estadoComponente = new NpgsqlParameter("estadoComponente", EnumConfig.GetDescription(TipoEstadoComponente.Ajuste));
                sqlParameters.Add(upaId);
                sqlParameters.Add(moduloId);
                sqlParameters.Add(estadoComponente);

                string consulta = $@"SELECT comp.*
                                        FROM laboratorio_lestoma.componente_laboratorio comp
                                               INNER JOIN superadmin.actividad actividad ON comp.actividad_id = actividad.id
                                         WHERE actividad.id IN ({string.Join(", ", parameters)})
                                               AND comp.descripcion_estado::JSONB->>'TipoEstado' <> @estadoComponente 
                                               AND comp.upa_id = @upaId 
                                               AND comp.modulo_componente_id = @moduloId";

                var componentes = _db.TablaComponentesLaboratorio.FromSqlRaw(consulta, sqlParameters.ToArray()).Include(y => y.Actividad);
                return await componentes.Select(x => new LaboratorioComponenteDTO
                {
                    Id = x.Id,
                    Actividad = x.Actividad.Nombre,
                    Nombre = x.NombreComponente,
                    DireccionRegistro = x.DireccionRegistro,
                    JsonEstado = x.JsonEstadoComponente
                }).ToListAsync();
            }

        }
    }
}

