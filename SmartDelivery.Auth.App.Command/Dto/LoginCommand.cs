namespace SmartDelivery.Auth.App.Command.Dto
{
    public class LoginCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Issuer { get; set; }
        public string Token { get; set; }
    }
}