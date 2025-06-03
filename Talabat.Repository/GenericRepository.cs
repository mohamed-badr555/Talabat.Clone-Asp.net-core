using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            ///Inner Join
            //if (typeof(T) == typeof(Product))
            //    return  (IEnumerable<T>) await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }

    

        public async Task<T?> GetById(int id)
        {
            //if(typeof(T) == typeof(Product))
            //    return await _dbContext.Set<Product>().Where(P => P.ID ==id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;


            return await _dbContext.Set<T>().FindAsync(id);
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
          return await ApplySpecification(spec).ToListAsync();
        } 
        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task AddAsync(T item)
        {
          await _dbContext.AddAsync(item);
        }

        public void Update(T item)
        {
            _dbContext.Update(item);    
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
        }
    }
}
