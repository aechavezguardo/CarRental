namespace Infraestructure.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransaction(CancellationToken cancellationToken);
        void Commit();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete(CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    }
}
