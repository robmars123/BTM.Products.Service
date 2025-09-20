namespace BTM.Products.Api.Factories.Abstractions
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Create(TSource source);
        IEnumerable<TDestination> Create(IEnumerable<TSource> source);
    }
}
