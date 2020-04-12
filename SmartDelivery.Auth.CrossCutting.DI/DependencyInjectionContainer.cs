using System;
using Microsoft.Extensions.DependencyInjection;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.App.Query.Dto;
using SmartDelivery.Auth.App.Query.Handlers;
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
            services.AddSingleton<IUserRepository>(r => new UserRepository("mongodb://192.168.99.100:27017/SmartDeliveryAuthTestDb")); 
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<LoginService>();
        }

        public void RegisterCommands(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddTransient<ICommandHandler<LoginCommand>, LoginCommandHandler>();
            services.AddTransient<IQueryHandler<GetUserByTokenQuery, GetUserByTokenInfo>, GetUserByTokenQueryHandler>();
        }
    }
}
