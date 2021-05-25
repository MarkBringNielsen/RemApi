using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RemApi.Data;

namespace RemApi.Services
{
    public class CreateService<T> : PersistenceService<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly RemaDbContext _context;

        public CreateService(DbSet<T> dbSet, RemaDbContext context) : base(dbSet, context)
        {
            _dbSet = dbSet;
            _context = context;
        }

        public async Task Create(T createItem)
        {
            await _dbSet.AddAsync(createItem);
            await MakeChange();
        }

    }
}