using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RemApi.Data;
using Microsoft.AspNetCore.Mvc;



namespace RemApi.Services
{
    public class DeleteService<T> : PersistenceService<T> where T : class 
    {
        private readonly DbSet<T> _dbSet;
        private readonly RemaDbContext _context;

        public DeleteService(DbSet<T> dbSet, RemaDbContext context) : base(dbSet, context)
        {
            _dbSet = dbSet;
            _context = context;
        }

        public async Task DeleteAsync(Guid id)
        {
            T deleteItem = await _dbSet.FindAsync(id);
            _dbSet.Remove(deleteItem);
            await MakeChange();  
        }

    }
}