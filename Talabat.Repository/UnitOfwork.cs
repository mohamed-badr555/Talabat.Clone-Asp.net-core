﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfwork : IUnitofWork
    {
        private readonly StoreDbContext _dbContext;
        private Hashtable _repositories;
  
        public UnitOfwork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if(!_repositories.Contains(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key, repository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;
        }
        public async Task<int> CompleteAsync()
      => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
           => await _dbContext.DisposeAsync();


    }
}
