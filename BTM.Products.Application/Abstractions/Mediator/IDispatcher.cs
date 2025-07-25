﻿namespace BTM.Products.Application.Abstractions.Mediator
{
    public interface IDispatcher
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> Send<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>;
    }

}
