namespace BTM.Products.Domain.Entities
{
    public abstract class Entity<TId>
    {
        public TId Id { get; init; }
    }
}
