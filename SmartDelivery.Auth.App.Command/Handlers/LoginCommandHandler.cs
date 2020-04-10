using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.Domain.Repositories;
using SmartDelivery.Auth.Domain.Services;
using SmartDelivery.Auth.Domain.Model;
using System;

namespace SmartDelivery.Auth.App.Command.Handlers
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand>
    {
        IUserRepository UserRepository;
        LoginService LoginService;

        public LoginCommandHandler(IUserRepository userRepository, LoginService loginService)
        {
            UserRepository = userRepository;
            LoginService = loginService;
        }

        public void Handle(LoginCommand command)
        {
            var user = UserRepository.Get(new User(null, null, command.Email, command.Password));

            if (user != null && string.IsNullOrEmpty(user.Id))
            {
                var login = new Login();
                login.SetPayload(user.Id, "test", DateTime.Now, user.Email);

                command.Token = LoginService.GenerateToken(login);
            }
        }
    }
}