using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> ExisteUpa(string nombre)
        {
            return await _db.TablaUpas.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()));
        }
    }
}
