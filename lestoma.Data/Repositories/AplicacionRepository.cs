using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class AplicacionRepository: GenericRepository<EAplicacion>
    {
        private readonly LestomaContext _db;
        public AplicacionRepository(LestomaContext db)
            : base(db)
        {
            _db = db;
        }
    }
}
