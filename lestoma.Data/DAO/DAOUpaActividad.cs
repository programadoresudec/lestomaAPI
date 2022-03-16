using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<Response> CrearVarios(EUpaActividad entidad)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in entidad.Actividades)
                    {
                        entidad.ActividadId = item.Id;
                        await Create(entidad);
                    }
                    transaction.Commit();
                    return new Response
                    {
                        IsExito = true,
                        Mensaje = "Se ha creado satisfactoriamente.",
                        StatusCode = (int)HttpStatusCode.Created
                    };      
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"{ex.Message}");
                }    
            }
        }

        public IQueryable<EUpaActividad> GetDetalleUpaActividad()
        {
  
            var listaAgrupada = _db.TablaUpasConActividades.Include(a => a.Actividad).GroupBy(p => new { p.UsuarioId, p.UpaId });

            //var listaAgrupada = (await _db.TablaUpasConActividades.Include(a => a.Actividad).ToListAsync())
            //    .GroupBy(p => new { p.UsuarioId, p.UpaId });

            if (listaAgrupada.Count() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            var query = listaAgrupada.Select(x => new EUpaActividad
            {
                UpaId = x.Key.UpaId,    
                UsuarioId = x.Key.UsuarioId,
                Upa = _db.TablaUpas.Find(x.Key.UpaId),
                FechaCreacion = x.Select(f => f.FechaCreacion).FirstOrDefault(),
                Ip = x.Select(i => i.Ip).FirstOrDefault(),
                Session = x.Select(s => s.Session).FirstOrDefault(),
                TipoDeAplicacion = x.Select(a => a.TipoDeAplicacion).FirstOrDefault(),
                Usuario = _db.TablaUsuarios.Include(r => r.Rol).FirstOrDefault(u => u.Id == x.Key.UsuarioId),
                Actividades = x.ToList().Select(y => new ActividadRequest
                {
                    Nombre = y.Actividad.Nombre,
                    Id = y.Actividad.Id
                }).ToList()
            });
            return query;
        }
    }
}

