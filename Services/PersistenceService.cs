using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RemApi.Data;



namespace RemApi.Services
{
    public class PersistenceService<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly RemaDbContext _context;

        public PersistenceService(DbSet<T> dbSet, RemaDbContext context)
        {
            _dbSet = dbSet;
            _context = context;
        }

        
        protected async Task MakeChange()
        {
            await _context.SaveChangesAsync();
        }

    }
}