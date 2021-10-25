using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOUpa : GenericRepository<EUpa>
    {
        private readonly Mapeo _db;
        public DAOUpa(Mapeo db) : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExisteUpa(string nombre, bool insertOrUpdate = false, int id = 0)
        {
            if (!insertOrUpdate)
            {
                return await _db.TablaUpas.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()));
            }
            else
            {
                return await _db.TablaUpas.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()) && x.Id != id);
            }

        }
        public async Task<ESuperAdministrador> GetSuperAdmin()
        {
            var user = await _db.TablaUsuarios.FirstOrDefaultAsync(x => x.RolId == (int)TipoRol.SuperAdministrador);
            if (user == null)
            {
                return null;
            }
            return await _db.TablaSuperAdministradores.FirstOrDefaultAsync(x => x.UsuarioId == user.Id);
        }

        public List<NameDTO> GetUpasJustNames()
        {
            var users = _db.TablaUpas.FromSqlRaw("SELECT id, nombre_upa FROM superadmin.upa").OrderBy(x => x.Nombre);
            var query = users.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
            }).ToList();
            return query;
        }
    }
}
