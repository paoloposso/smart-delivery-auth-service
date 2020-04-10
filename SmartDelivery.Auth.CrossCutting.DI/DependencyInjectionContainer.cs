using System;
using Microsoft.Extensions.DependencyInjection;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.Domain.Repositories;
using SmartDelivery.Auth.Domain.Services;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb;

namespace SmartDelivery.Auth.CrossCutting.DI
{
    public class DependencyInjectionContainer
    {
        public void Register(IServiceCollection services) {
            RegisterCommands(services);
            RegisterServices(services);
            services.AddTransient<IUserRepository, UserRepository>(); 
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<LoginService>();
        }

        public void RegisterCommands(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddTransient<ICommandHandler<LoginCommand>, LoginCommandHandler>();
        }
    }
}
