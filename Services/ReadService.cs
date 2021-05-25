using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;



namespace RemApi.Services
{
    public class ReadService<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public ReadService(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

    }
}