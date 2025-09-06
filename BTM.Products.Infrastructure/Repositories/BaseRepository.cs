using BTM.Products.Domain.Entities;
using BTM.Products.Infrastructure.Connection;

namespace BTM.Products.Infrastructure.Repositories
{
    public class BaseRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext DbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await DbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public virtual void Remove(T entity, CancellationToken cancellationToken)
        {
            DbContext.Remove(entity);
        }
    }
}
