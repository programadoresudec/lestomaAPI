using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class ProtocoloRepository : GenericRepository<EProtocoloCOM>
    {
        private readonly LestomaContext _db;
        public ProtocoloRepository(LestomaContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<NameProtocoloDTO>> GetProtocolsJustNames()
        {
            var query = await _dbSet.Select(x => new NameProtocoloDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,

            }).OrderBy(x => x.Nombre).ToListAsync();
            return query;
        }
    }
}
