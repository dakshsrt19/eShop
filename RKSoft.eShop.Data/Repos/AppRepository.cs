using Microsoft.EntityFrameworkCore;
using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Data.Context;
using System.Linq.Expressions;

namespace RKSoft.eShop.Data.Repositories
{
    public class AppRepository<T> : IAppRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<T> _dbSet;
        public AppRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }
        public async Task<T> CreateAsync(T dbRecord)
        {
            _dbSet.Add(dbRecord);
            await _appDbContext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet?.Remove(dbRecord);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetByNameAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T dbRecord)
        {
            _appDbContext.Update(dbRecord);
            await _appDbContext.SaveChangesAsync();
            return dbRecord;
        }
    }
}