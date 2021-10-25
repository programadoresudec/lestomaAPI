using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOActividad : GenericRepository<EActividad>
    {
        private readonly Mapeo _db;
        public DAOActividad(Mapeo db)
            : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExisteActividad(string nombre, bool insertOrUpdate = false, int id = 0)
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

        public List<NameDTO> GetActividadesJustNames()
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
