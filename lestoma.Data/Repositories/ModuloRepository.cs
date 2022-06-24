using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
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
                return await _db.TablaModuloComponentes.AnyAsync(x => x.Nombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            }
            else
            {
                return await _db.TablaModuloComponentes.AnyAsync(x => x.Nombre.ToLower().Trim().
                Equals(nombre.ToLower().Trim()) && x.Id != id);
            }

        }
    }
}