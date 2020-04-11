using System;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.Domain.Repositories;
using SmartDelivery.Auth.Domain.Model;
using System.Text;

namespace SmartDelivery.Auth.App.Command.Handlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(CreateUserCommand command) 
        {
            ValidateUserInsertion(command);

            var user = new User(command.FullName, command.Document, command.Email, command.Password);
            
            _userRepository.Insert(user);

            command.Id = user.Id;
        }

        private void ValidateUserInsertion(CreateUserCommand command)
        {
            var errors = new StringBuilder();
            
            if (string.IsNullOrEmpty(command.Document))
                errors.Append("Document is required");
            if (string.IsNullOrEmpty(command.FullName))
                errors.Append("FullName is required");
            if (string.IsNullOrEmpty(command.Password))
                errors.Append("Password is required");
            if (string.IsNullOrEmpty(command.Email))
                errors.Append("Email is required");

            if (errors.Length > 0)
                throw new ArgumentException(errors.ToString());
        }
    }
}
