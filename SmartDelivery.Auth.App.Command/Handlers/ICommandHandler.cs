using System;
using SmartDelivery.Auth.App.Command.Dto;

namespace SmartDelivery.Auth.App.Command.Handlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}
