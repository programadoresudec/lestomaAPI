using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOLaboratorio : GenericRepository<ELaboratorio>
    {
        private readonly Mapeo _db;
        public DAOLaboratorio(Mapeo db) : base(db)
        {
            _db = db;
        }
    }
}

    