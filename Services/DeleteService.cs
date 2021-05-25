using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;



namespace RemApi.Services
{
    public class GetService<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GetService(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

    }
}