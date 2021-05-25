using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RemApi.Data;

namespace RemApi.Services
{
    public class UpdateService<T> : PersistenceService<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly RemaDbContext _context;

        public UpdateService(DbSet<T> dbSet, RemaDbContext context) : base(dbSet, context)
        {
            _dbSet = dbSet;
            _context = context;

        }

        public async Task Update(T updateItem)
        {
            _context.Entry(updateItem).State = EntityState.Modified;

            try
            {
                await MakeChange();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

    }
}