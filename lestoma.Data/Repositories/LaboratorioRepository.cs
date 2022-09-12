using lestoma.CommonUtils.DTOs;
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
    }
}

