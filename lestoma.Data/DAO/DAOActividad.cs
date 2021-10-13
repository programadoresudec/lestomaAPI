﻿using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOActividad : GenericRepository<EActividad>
    {
        private readonly Mapeo _db;
        public DAOActividad(Mapeo db)
            : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExisteActividad(string nombre, bool insertOrUpdate = false, int id = 0)
        {
            if (insertOrUpdate)
            {
                return await _db.TablaActividades.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()));
            }
            else
            {
                return await _db.TablaActividades.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()) && x.Id != id);
            }

        }
    }
}
