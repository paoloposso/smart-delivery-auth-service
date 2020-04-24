using System;
using Microsoft.Extensions.DependencyInjection;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.App.Query.Dto;
using SmartDelivery.Auth.App.Query.Handlers;
using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Domain.Repositories;
using SmartDelivery.Auth.Domain.Services;
using SmartDelivery.Auth.Domain.Services.Strategies.TokenGeneration;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb;

namespace SmartDelivery.Auth.CrossCutting.DI
{
    public class DependencyInjectionContainer
    {
        private AppSettings _appSettings;

        public DependencyInjectionContainer(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Register(IServiceCollection services)
        {
            RegisterCommands(services);
            RegisterServices(services);
            services.AddSingleton<IUserRepository, UserRepository>(); 
            services.AddTransient<AppSettings>(r => _appSettings);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<LoginService>();
            services.AddTransient<ITokenGeneratorStrategy>(s => new JwtTokenGeneratorStrategy(_appSettings.JwtSecret));
        }

        public void RegisterCommands(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddTransient<ICommandHandler<LoginCommand>, LoginCommandHandler>();
            services.AddTransient<IQueryHandler<GetUserByTokenQuery, GetUserByTokenInfo>, GetUserByTokenQueryHandler>();
        }
    }
}
