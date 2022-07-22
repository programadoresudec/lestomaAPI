using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace lestoma.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LestomaContext _context;
        private DbSet<T> _entities;

        public GenericRepository(LestomaContext context)
        {
            this._context = context;
            _entities = context.Set<T>();
        }

        #region Listado IEnumerable In BD
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        #endregion

        #region Listado IQueryable para hacer consultas al servidor optimizado
        public IQueryable<T> GetAllAsQueryable()
        {
            return _entities.AsNoTracking();
        }

        #endregion

        #region Get by Id In BD
        public async Task<T> GetById(object id)
        {
            return await _entities.FindAsync(id);
        }
        #endregion

        #region Create In BD
        public async Task Create(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException($"{nameof(entidad)} no debe ser nula");
            try
            {
                await _context.AddAsync(entidad);
                await SaveAllAsync();
            }
            catch (Exception ex)
            {
                ObtenerException(ex, entidad);
            }
        }
        #endregion

        #region Update In BD
        public async Task Update(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException($"{nameof(entidad)} no debe ser nula");
            try
            {
                _context.Update(entidad);
                await SaveAllAsync();
            }
            catch (Exception ex)
            {
                ObtenerException(ex, entidad);
            }
        }
        #endregion

        #region Delete In BD

        public async Task Delete(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException($"{nameof(entidad)} no debe ser nula");
            try
            {
                _context.Remove(entidad);
                await SaveAllAsync();
            }
            catch (Exception ex)
            {
                ObtenerException(ex, entidad);
            }
        }
        #endregion

        #region Exist with condition
        public async Task<bool> AnyWithCondition(Expression<Func<T, bool>> whereCondition)
        {
            return await _entities.AnyAsync(whereCondition);
        }
        #endregion

        #region Find with condition
        public async Task<T> FindWithCondition(Expression<Func<T, bool>> whereCondition)
        {
            return await _entities.FirstOrDefaultAsync(whereCondition);
        }
        #endregion

        #region Save In BD
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Exception Generic
        public static TException GetInnerException<TException>(Exception exception)
          where TException : Exception
        {
            Exception innerException = exception;
            while (innerException != null)
            {
                if (innerException is TException result)
                {
                    return result;
                }
                innerException = innerException.InnerException;

            }
            return null;
        }

        public void ObtenerException(Exception ex, T entidad)
        {
            var udpateException = GetInnerException<DbUpdateException>(ex);
            var pgsqlException = GetInnerException<PostgresException>(ex);
            if (udpateException != null)
            {
                throw new Exception($"{nameof(entidad)} no se ha podido crear: {udpateException}");
            }
            else if (pgsqlException != null)
            {
                throw new Exception($"{nameof(entidad)} no se ha podido crear: {pgsqlException}");
            }
            else
            {
                throw new Exception($"{nameof(entidad)} {ex.Message}");
            }
        }
        #endregion

        #region Merge
        public async Task Merge(List<T> ListadoEntidad)
        {
            CancellationTokenSource tcs = new();
            CancellationToken token = new();
            try
            {
                List<T> listadoNuevo = new();
                foreach (var item in ListadoEntidad)
                {
                    var Property = item.GetType().GetProperty("Id");
                    var entidad = await GetById(Property.GetValue(item, null));
                    if (entidad == null)
                    {
                        _context.Entry(item).State = EntityState.Added;
                        listadoNuevo.Add(item);
                    }
                    else
                    {
                        _context.Entry(entidad).State = EntityState.Modified;
                        listadoNuevo.Add(entidad);
                    }
                }
                await _context.SaveChangesAsync(true, token);

            }
            catch (Exception ex)
            {
                ObtenerException(ex, null);
            }
            finally
            {
                tcs.Cancel();
            }
        }
        #endregion
    }
}
