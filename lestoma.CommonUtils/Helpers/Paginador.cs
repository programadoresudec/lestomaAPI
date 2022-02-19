using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Helpers
{
    public class Paginador<T>
    {
        public Paginacion paginacion { get; set; } = new Paginacion();
        public int TotalPages { get; set; }
        public int TotalDatos { get; set; }
        public List<T> Datos { get; set; } = new List<T>();

        public Paginador()
        {

        }
        public Paginador(List<T> items, int count, Paginacion paginacion)
        {
            this.paginacion = paginacion;
            this.TotalDatos = count;
            TotalPages = (int)Math.Ceiling(count / (double)paginacion.PageSize);
            this.Datos.AddRange(items);

        }

        public bool HasPreviousPage
        {
            get
            {
                return (this.paginacion.Page > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (this.paginacion.Page < TotalPages);
            }
        }

        public static Paginador<T> CrearPaginador(int count, IEnumerable<T> source, Paginacion paginacion)
        {
            var items = source.ToList();
            return new Paginador<T>(items, count, paginacion);
        }
    }
}
