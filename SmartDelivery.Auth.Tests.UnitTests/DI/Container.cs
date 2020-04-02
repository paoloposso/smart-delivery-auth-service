using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.Domain.Repositories;
using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.Tests.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace SmartDelivery.Auth.Tests.UnitTests.DI
{
    public static class Container
    {
        public static void RegisterDependencies()
        {
            var services = new ServiceCollection();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
        }
    }
}