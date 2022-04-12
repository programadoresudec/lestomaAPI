using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOComponente : GenericRepository<EComponentesLaboratorio>
    {
        private readonly Mapeo _db;

        public DAOComponente(Mapeo db) : base(db)
        {
            _db = db;
        }


        public List<NameDTO> GetComponentesJustNames()
        {
            var comp = _db.TablaComponentesLaboratorio.FromSqlRaw("SELECT id, nombre FROM laboratorio_lestoma.componente_laboratorio").OrderBy(x => x.Nombre);
            var query = comp.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,

            }).ToList();
            return query;

        }

    }
}

