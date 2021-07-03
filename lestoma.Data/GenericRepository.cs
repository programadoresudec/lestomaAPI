using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
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

        public async Task<T> GetByIdAsync(object id)
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
    }
}
