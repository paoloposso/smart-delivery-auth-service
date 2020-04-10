using System;
using System.Text;
using SmartDelivery.Auth.Domain.Model;

namespace SmartDelivery.Auth.Domain.Services
{
    public class LoginService
    {
        public string GenerateToken(Login login) 
        {
            ValidateLogin(login);

            return "aaaaaaa";
        }

        private void ValidateLogin(Login login)
        {
            var sb = new StringBuilder();

            if (login.Payload == null)
                throw new ApplicationException("Payload is Required");

            if (string.IsNullOrEmpty(login.Payload.Email))
                sb.AppendLine("Email is required");
            if (string.IsNullOrEmpty(login.Payload.Issuer))
                sb.AppendLine("Issuer is required");
            if (string.IsNullOrEmpty(login.Payload.Subject))
                sb.AppendLine("Subject is required");
            if (login.Payload.Expiration == null || login.Payload.Expiration < DateTime.Now)
                sb.AppendLine($"Expiration {login.Payload.Expiration} is invalid");

            if (sb.Length > 0)
                throw new ApplicationException(sb.ToString());
        }
    }
}
