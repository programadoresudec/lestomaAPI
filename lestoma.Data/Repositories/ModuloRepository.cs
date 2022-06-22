using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class ModuloRepository : GenericRepository<EModuloComponente>
    {
        private readonly LestomaContext _db;
        public ModuloRepository(LestomaContext db) : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExisteModulo(string nombre, int id, bool insertOrUpdate = false)
        {
            if (!insertOrUpdate)
            {
                return await _db.TablaModulo.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()));
            }
            else
            {
                return await _db.TablaModulo.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()) && x.Id != id);
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

        public List<NameDTO> GetModuloJustNames()
        {
            var users = _db.TablaModulo.FromSqlRaw("SELECT id, nombre_upa FROM superadmin.upa").OrderBy(x => x.Nombre);
            var query = users.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
            }).ToList();
            return query;
        }
    }
}