using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PortfolioHub.Domain.RepositoryContract;
using System.Linq.Expressions;
using System.Reflection;

namespace PortfolioHub.Infrastructure.Efcore.RepositoryProvider
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext dbContext;

        private readonly DbSet<T> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Get all records from DB
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        /// <summary>
        /// Add records in DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        /// <summary>
        /// delete record based on entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// query to fetch records from DB
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> FindByPredicate(Expression<Func<T, bool>> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate);
        }
        
        /// <summary>
        /// Save Changes to DB 
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update specific entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(entity);
        }
      
        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Delete(IEnumerable<T> entities) => dbSet.RemoveRange(entities);
    }
}