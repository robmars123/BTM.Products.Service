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

        public virtual void Add(T entity, CancellationToken cancellationToken)
        {
            DbContext.AddAsync(entity, cancellationToken);
        }

        public virtual void Remove(T entity, CancellationToken cancellationToken)
        {
            DbContext.Remove(entity);
        }
    }
}
