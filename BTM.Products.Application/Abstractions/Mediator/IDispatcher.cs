namespace BTM.Products.Application.Abstractions.Mediator
{
    public interface IDispatcher
    {
        Task Send(ICommand command);
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }

}
