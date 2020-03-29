using System;
using SmartDelivery.Auth.App.Command.Dto;

namespace SmartDelivery.Auth.App.Command.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        void Handle(T command);
    }
}
