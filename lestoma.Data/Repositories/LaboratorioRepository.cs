using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.DTOs.Sync;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

        public async Task<IEnumerable<NameDTO>> GetModulesByUpaActivitiesUserId(UpaActivitiesFilterRequest filtro)
        {
            var query = await (from componente in _db.TablaComponentesLaboratorio
                               join modulo in _db.TablaModuloComponentes on componente.ModuloComponenteId equals modulo.Id
                               where componente.UpaId == filtro.UpaId && filtro.ActividadesId.Contains(componente.ActividadId)
                               group modulo by new { modulo.Id, modulo.Nombre } into grupo
                               select new NameDTO
                               {
                                   Id = grupo.Key.Id,
                                   Nombre = grupo.Key.Nombre
                               }).ToListAsync();
            return query;
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

        public async Task<IEnumerable<LaboratorioComponenteDTO>> GetComponentsByModuleId(Guid id)
        {
            var query = await (from componente in _db.TablaComponentesLaboratorio
                               join actividad in _db.TablaActividades on componente.ActividadId equals actividad.Id
                               where componente.ModuloComponenteId == id
                               select new LaboratorioComponenteDTO
                               {
                                   Actividad = actividad.Nombre,
                                   Id = componente.Id,
                                   Nombre = componente.NombreComponente,
                                   JsonEstado = componente.JsonEstadoComponente
                               }).ToListAsync();
            return query;
        }
    }
}

