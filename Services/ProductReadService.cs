using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RemApi.Models;



namespace RemApi.Services
{
    public class ProductReadService
    {
        private readonly DbSet<Product> _dbSet;

        public ProductReadService(DbSet<Product> dbSet)
        {
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<Product>> GetAsync()
        { 
            return await _dbSet.Include(p => p.Category).Include(p => p.Supplier).ToListAsync();
        }
        public async Task<Product> GetAsync(Guid id)
        {
            return await _dbSet.Include(p => p.Supplier).Include(p => p.Category).FirstOrDefaultAsync(p => p.ID == id);
        }

    }
}