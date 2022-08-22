using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
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

