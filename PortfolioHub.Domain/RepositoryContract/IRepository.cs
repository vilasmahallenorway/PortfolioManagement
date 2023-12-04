using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace PortfolioHub.Domain.RepositoryContract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FindByPredicate(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
        void Delete(T entity);
        IQueryable<T> GetAll();
    }
}