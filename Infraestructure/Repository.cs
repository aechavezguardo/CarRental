using Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Infraestructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetAllAsync()
        {
            return _entities;
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _entities.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(predicate, cancellationToken);
        }

        public async Task AddMultipleAsync(IEnumerable<T> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public IQueryable<T> Find(List<Expression<Func<T, bool>>> predicates)
        {
            var query = _context.Set<T>().AsQueryable();

            if (predicates != null)
            {
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate);
                }
            }

            return query;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
        }

        public async Task DeleteAllAsync(List<T> entities)
        {
            _entities.RemoveRange(entities);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Expression<Func<T, bool>> CreateExpression<T, TValue>(string propertyName, TValue value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value);

            // Check if the property type is nullable
            if (Nullable.GetUnderlyingType(property.Type) != null)
            {
                // The property is nullable, so we need to call GetValueOrDefault
                var getValueOrDefaultMethod = property.Type.GetMethod("GetValueOrDefault", Type.EmptyTypes);
                var getValueOrDefaultCall = Expression.Call(property, getValueOrDefaultMethod);

                return Expression.Lambda<Func<T, bool>>(Expression.Equal(getValueOrDefaultCall, constant), parameter);
            }
            else
            {
                return Expression.Lambda<Func<T, bool>>(Expression.Equal(property, constant), parameter);
            }
        }
        public Expression<Func<T, bool>> CreateExpression<T, TValue>(string propertyName, IEnumerable<TValue> values)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(values);
            var containsMethod = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                                                    .MakeGenericMethod(typeof(TValue));
            var containsCall = Expression.Call(null, containsMethod, constant, property);

            return Expression.Lambda<Func<T, bool>>(containsCall, parameter);
        }


        public Expression<Func<T, bool>> CombineExpressions<T>(IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            var combinedExpression = expressions.First();
            foreach (var expression in expressions.Skip(1))
            {
                combinedExpression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(combinedExpression.Body, expression.Body), combinedExpression.Parameters);

            }
            return combinedExpression;
        }
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _entities.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> OrderByDescending(Expression<Func<T, int>> keySelector)
        {
            return _entities.OrderByDescending(keySelector);
        }

        public IQueryable<T> OrderByDescending(Expression<Func<T, long>> keySelector)
        {
            return _entities.OrderByDescending(keySelector);
        }
    }
}
