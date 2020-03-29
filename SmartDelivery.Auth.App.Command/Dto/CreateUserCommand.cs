using System;

namespace SmartDelivery.Auth.App.Command.Dto
{
    public class CreateUserCommand : ICommand
    {
        public string Email { get; set; }
        public string Document { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
