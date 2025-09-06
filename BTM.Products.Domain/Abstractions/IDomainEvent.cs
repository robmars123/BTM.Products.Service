namespace BTM.Products.Domain.Abstractions.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
