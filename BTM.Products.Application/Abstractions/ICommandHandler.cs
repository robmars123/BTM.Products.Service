﻿namespace BTM.Products.Application.Abstractions
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}
