using lestoma.CommonUtils.DTOs;
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

        public async Task<IEnumerable<DataComponentSyncDTO>> GetDataBySyncToMobileByUpaId(Guid upaId)
        {
            return await _db.TablaComponentesLaboratorio.Include(modulo => modulo.ModuloComponente)
                .Where(x => x.UpaId == upaId)
                .Select(x => new DataComponentSyncDTO
                {
                    ActividadId = x.ActividadId,
                    DescripcionEstadoJson = x.JsonEstadoComponente,
                    Id = x.Id,
                    UpaId = x.UpaId,
                    NombreComponente = x.NombreComponente,
                    FechaCreacionServer = x.FechaCreacionServer,
                    Session = x.Session,
                    TipoDeAplicacion = x.TipoDeAplicacion,
                    Ip = x.Ip,
                    Modulo = new ModuloDTO
                    {
                        Id = x.ModuloComponente.Id,
                        FechaCreacionServer = x.ModuloComponente.FechaCreacionServer,
                        Ip = x.ModuloComponente.Ip,
                        Nombre = x.ModuloComponente.Nombre,
                        Session = x.ModuloComponente.Session,
                        TipoDeAplicacion = x.ModuloComponente.TipoDeAplicacion
                    }
                }).ToListAsync();
        }

        public async Task<IEnumerable<NameDTO>> GetModulosByUpaAndActivitiesOfUser(UpaActivitiesFilterRequest filtro)
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

