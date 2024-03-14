using System.Linq.Expressions;

namespace Infraestructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAsync();
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        IQueryable<T> Find(List<Expression<Func<T, bool>>> predicates);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddMultipleAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAllAsync(List<T> entities);
        Task SaveAsync();
        Expression<Func<T, bool>> CreateExpression<T, TValue>(string propertyName, TValue value);
        Expression<Func<T, bool>> CreateExpression<T, TValue>(string propertyName, IEnumerable<TValue> values);
        Expression<Func<T, bool>> CombineExpressions<T>(IEnumerable<Expression<Func<T, bool>>> expressions);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        IQueryable<T> OrderByDescending(Expression<Func<T, int>> keySelector);
        IQueryable<T> OrderByDescending(Expression<Func<T, long>> keySelector);
    }
}
