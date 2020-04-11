using System;
using System.Text;
using SmartDelivery.Auth.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using SmartDelivery.Auth.Domain.Services.Strategies;

namespace SmartDelivery.Auth.Domain.Services
{
    public class LoginService
    {
        //TODO: refactor and separate into a strategy class
        private Func<Payload,string> GenarationAlgorythm;

        public LoginService()
        {
            GenarationAlgorythm += JwtStrategy.GenerateToken;
        }

        public string GetToken(Login login)
        {
            ValidateLogin(login);

            return GenarationAlgorythm(login.Payload);
        }

        private void ValidateLogin(Login login)
        {
            var sb = new StringBuilder();

            if (login.Payload == null)
                throw new ApplicationException("Payload is Required");

            if (string.IsNullOrEmpty(login.Payload.Email))
                sb.AppendLine("Email is required");
            if (string.IsNullOrEmpty(login.Payload.Iss))
                sb.AppendLine("Issuer is required");
            if (string.IsNullOrEmpty(login.Payload.Sub))
                sb.AppendLine("Subject is required");
            if (login.Payload.Exp == null || login.Payload.Exp < DateTime.Now)
                sb.AppendLine($"Expiration {login.Payload.Exp} is invalid");

            if (sb.Length > 0)
                throw new ApplicationException(sb.ToString());
        }
    }
}
