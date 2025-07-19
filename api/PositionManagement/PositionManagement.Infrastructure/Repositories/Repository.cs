using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PositionManagement.Infrastructure.Context;

namespace PositionManagement.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        protected readonly TradeContext _tradeContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
            _dbSet = _tradeContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id).AsTask();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity).AsTask();

        public void Update(T entity) => _dbSet.Update(entity);

        public void Remove(T entity) => _dbSet.Remove(entity);
    }
}
