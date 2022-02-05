using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lestoma.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly Mapeo _context;
        private DbSet<T> _entities;

        public GenericRepository(Mapeo context)
        {
            this._context = context;
            _entities = context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }
        public IQueryable<T> GetAllAsQueryable()
        {
            return _entities.AsQueryable();
        }
        public async Task<T> GetById(object id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Create(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException($"{nameof(entidad)} no debe ser nula");
            try
            {
                _context.Add(entidad);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var pgsqlException = GetInnerException<PostgresException>(ex);
                if (pgsqlException != null)
                {
                    throw new Exception($"{nameof(entidad)} no se ha podido crear: {pgsqlException}");
                }
                else
                {
                    throw new Exception($"{nameof(entidad)} no se ha podido crear: {ex.Message}");
                }
            }
        }

        public async Task Update(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException($"{nameof(entidad)} no debe ser nula");
            try
            {
                _context.Update(entidad);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var pgsqlException = GetInnerException<PostgresException>(ex);
                if (pgsqlException != null)
                {
                    throw new Exception($"{nameof(entidad)} no se ha podido actualizar: {pgsqlException}");
                }
                else
                {
                    throw new Exception($"{nameof(entidad)} no se ha podido actualizar: {ex.Message}");
                }
            }
        }

        public async Task Delete(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException($"{nameof(entidad)} no debe ser nula");
            try
            {
                _context.Remove(entidad);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var pgsqlException = GetInnerException<PostgresException>(ex);
                if (pgsqlException != null)
                {
                    throw new Exception($"{nameof(entidad)} no se ha podido eliminar: {pgsqlException}");
                }
                else
                {
                    throw new Exception($"{nameof(entidad)} no se ha podido eliminar: {ex.Message}");
                }
            }
        }
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

        public async Task Merge(List<T> ListadoEntidad)
        {
            CancellationTokenSource tcs = new CancellationTokenSource();
            CancellationToken token = new CancellationToken();
            try
            {
                List<T> listadoNuevo = new List<T>();
                foreach (var item in ListadoEntidad)
                {
                    var Property = item.GetType().GetProperty("Id");
                    var entidad = await GetById(Property.GetValue(item, null));
                    if (entidad == null)
                    {
                        _context.Entry(item).State = EntityState.Added;
                        _context.ProcesarAuditoria();
                        listadoNuevo.Add(item);
                    }
                    else
                    {
                        _context.Entry(entidad).State = EntityState.Modified;
                        _context.ProcesarAuditoria();
                        listadoNuevo.Add(entidad);
                    }
                }
                await _context.BulkSynchronizeAsync(listadoNuevo, token);

            }
            catch (Exception ex)
            {

                var SQLiteException = GetInnerException<PostgresException>(ex);
                if (SQLiteException != null)
                {
                    throw new Exception($"{nameof(ListadoEntidad)} no se ha podido mezclar: {SQLiteException}");
                }
                else
                {
                    throw new Exception($"{nameof(ListadoEntidad)} no se ha podido mezclar: {ex.Message}");
                }
            }
            finally
            {
                tcs.Cancel();
            }
        }
    }
}
