using System;
using Microsoft.Extensions.DependencyInjection;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.Domain.Repositories;
using SmartDelivery.Auth.Tests.Repository;

namespace SmartDelivery.Auth.CrossCutting.DI
{
    public class DependencyInjectionContainer
    {
        public void Register(IServiceCollection services) {
            RegisterCommands(services);
            services.AddTransient<IUserRepository, UserRepository>(); 
        }

        public void RegisterCommands(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
        }
    }
}
