using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<IEnumerable<NameProtocoloDTO>> GetProtocolsByUpaId(Guid upaId)
        {
            return await _dbSet.Where(x => x.UpaId == upaId).Select(x => new NameProtocoloDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                PrimerByteTrama = x.PrimerByteTrama
            }).OrderBy(x => x.Nombre).ToListAsync();
        }
    }
}
