using lestoma.CommonUtils.Enums;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
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
    }
}
