using SmartDelivery.Auth.App.Command.Dto;

namespace SmartDelivery.Auth.App.Command.Handlers
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand>
    {
        public void Handle(LoginCommand command)
        {
            command.Token = "2685376423Fdsjfdkfdso";
        }
    }
}