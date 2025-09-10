using BTM.Products.Domain.Entities;
using BTM.Products.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

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

        public virtual void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }
    }
}
