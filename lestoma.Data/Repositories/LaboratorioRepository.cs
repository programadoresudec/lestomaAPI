using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.DTOs.Sync;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<IEnumerable<DataOnlineSyncDTO>> GetDataBySyncToMobileByUpaId(UpaActivitiesFilterRequest filtro, bool isSuperAdmin)
        {

            if (!isSuperAdmin)
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
                try
                {
                    var parameters = new string[filtro.ActividadesId.Count()];
                    var sqlParameters = new List<NpgsqlParameter>();
                    for (var i = 0; i < filtro.ActividadesId.Count(); i++)
                    {
                        parameters[i] = string.Format("@p{0}", i);
                        sqlParameters.Add(new NpgsqlParameter(parameters[i], filtro.ActividadesId.ToList()[i]));
                    }

                    var upaId = new NpgsqlParameter("upaId", filtro.UpaId);
                    var estadoComponente = new NpgsqlParameter("estadoComponente", "AJUSTE");
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
                catch (Exception ex)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, @$"Error: {ex.Message}");
                }
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
            return await _db.TablaDetalleLaboratorio.Where(x => x.ComponenteLaboratorioId == id).OrderByDescending(d => d.FechaCreacionDispositivo)
                          .Select(x => new TramaComponenteDTO
                          {
                              TramaInPut = x.TramaEnviada,
                              TramaOutPut = x.TramaRecibida,
                              SetPointIn = x.ValorCalculadoTramaEnviada,
                              SetPointOut = x.ValorCalculadoTramaRecibida
                          }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByActivitiesOfUpaUserId(UpaActivitiesModuleFilterRequest filtro)
        {
            var query = await (from componente in _db.TablaComponentesLaboratorio
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
            return query;
        }
    }
}

