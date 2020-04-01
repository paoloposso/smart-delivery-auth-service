using System;
using Microsoft.Extensions.DependencyInjection;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.Domain.Repositories;

namespace SmartDelivery.Auth.CrossCutting.DI
{
    public class DependencyInjectionContainer
    {
        public void Register(IServiceCollection services) {
            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
        }
    }
}
