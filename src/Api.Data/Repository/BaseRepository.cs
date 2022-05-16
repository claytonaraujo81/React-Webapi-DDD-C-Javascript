using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entitiers;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class NewBaseType
    {
        private readonly object _context;
        private object _Dataset;

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                item.CreateAt = DateTime.UtcNow;
                _Dataset.Add(item);
                object value = _context.SaveChangeAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }
    }

    public class T
    {
        internal Guid Id;

        public DateTime CreateAt { get; internal set; }
    }

    public class BaseRepository<T> : NewBaseType, IRepository<T> where T : BaseEntity
    {

        protected readonly MyContext _context;
        private readonly Guid id;
        private DbSet<T> _Dataset;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _Dataset = _context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var result = await _Dataset.SingleOrDefaultAsync(p => p.Id.Equals(Id));
                if (result == null)
                    return false;

                _Dataset.Remove(result);
                await _context.SaveChangeAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExistAsync(Guid Id)
        {
            return await _Dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }

            throw new NotImplementedException();
        }

        public Task<T> SelectAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _Dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                    return null;

                item.UpdateAt = DateTime.UtcNow;
                DateTime? createAt = result.CreateAt;
                item.CreateAt = createAt;

                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangeAsync();
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;


        }
    }
}
