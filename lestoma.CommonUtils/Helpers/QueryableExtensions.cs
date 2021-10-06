using System.Linq;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginacion paginacion)
        {
            return queryable
                .Skip((paginacion.Page - 1) * paginacion.PageSize)
                .Take(paginacion.PageSize);
        }
    }
}