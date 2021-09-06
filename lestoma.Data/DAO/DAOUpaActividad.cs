using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOUpaActividad : GenericRepository<EUpaActividad>
    {
        private readonly Mapeo _db;
        public DAOUpaActividad(Mapeo db) : base(db)
        {
            _db = db;
        }
    }
}
