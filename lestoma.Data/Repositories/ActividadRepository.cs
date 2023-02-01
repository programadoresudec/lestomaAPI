using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class ActividadRepository : GenericRepository<EActividad>
    {
        private readonly LestomaContext _db;
        public ActividadRepository(LestomaContext db)
            : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExistActivity(string nombre, Guid id, bool insertOrUpdate = false)
        {
            if (!insertOrUpdate && id == Guid.Empty)
            {
                return await _dbSet.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()));
            }
            else
            {
                return await _dbSet.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()) && x.Id != id);
            }

        }

        public async Task<IEnumerable<NameDTO>> GetActivitiesJustNames()
        {
            var query = await _dbSet.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
            }).OrderBy(x => x.Nombre).ToListAsync();
            return query;
        }
    }
}