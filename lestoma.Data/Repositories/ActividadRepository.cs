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
            if (insertOrUpdate)
            {
                return await _db.TablaActividades.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()));
            }
            else
            {
                return await _db.TablaActividades.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()) && x.Id != id);
            }

        }

        public List<NameDTO> GetActivitiesJustNames()
        {
            var users = _db.TablaActividades.FromSqlRaw("SELECT id, nombre_actividad FROM superadmin.actividad").OrderBy(x => x.Nombre);
            var query = users.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
            }).ToList();
            return query;
        }
    }
}