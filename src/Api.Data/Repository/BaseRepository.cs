using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Entitiers;
using Api.Data.Context;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    // public class NewBaseType
    // {
    //     private readonly MyContext _context;

    //     public async Task<T> InsertAsync(T item)
    //     {
    //         try
    //         {
    //             if (item.Id == Guid.Empty)
    //             {
    //                 item.Id = Guid.NewGuid();
    //             }

    //             item.CreateAt = DateTime.UtcNow;
    //             _context.Add(item);
    //             object value = _context.SaveChangeAsync();

    //         }
    //         catch (Exception ex)
    //         {
    //             throw ex;
    //         }
    //         return item;
    //     }
    // }

    // public class T
    // {
    //     internal Guid Id;

    //     public DateTime CreateAt { get; internal set; }
    // }

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected readonly MyContext _context;
        private readonly Guid _id; // rever como sera usado
        private DbSet<T> _dataset;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(Id));
                if (result == null)
                    return false;

                _dataset.Remove(result);
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
            return await _dataset.AnyAsync(p => p.Id.Equals(_id));
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                item.CreateAt = DateTime.UtcNow;
                _context.Add(item);
                await _context.SaveChangeAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
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
            // try
            // {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result == null)
                return null;

            item.UpdateAt = DateTime.UtcNow;
            DateTime? createAt = result.CreateAt;
            item.CreateAt = createAt;

            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangeAsync();
            {

            }
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            // }
            return item;


        }
    }
}
