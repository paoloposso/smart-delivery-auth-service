using System;
using System.Text;
using SmartDelivery.Auth.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using SmartDelivery.Auth.Domain.Services.Strategies;
using SmartDelivery.Auth.Domain.Services.Strategies.TokenGeneration;

namespace SmartDelivery.Auth.Domain.Services
{
    public class LoginService
    {
        //TODO: refactor and separate into a strategy class and abstract the strategy
        private Func<Payload,string> TokenGenarationAlgorythm;
        private Func<string,Payload> GetPayloadByTokenAlgorythm;

        ITokenGeneratorStrategy _tokenGenerationStrategy;

        public LoginService()
        {
            _tokenGenerationStrategy = new JwtTokenGeneratorStrategy();
            TokenGenarationAlgorythm += _tokenGenerationStrategy.GenerateToken;
            GetPayloadByTokenAlgorythm += _tokenGenerationStrategy.GetPayloadByToken;
        }

        public Payload GetUserInfoByToken(string token)
        {
            return GetPayloadByTokenAlgorythm?.Invoke(token);
        }

        public string GetToken(Login login)
        {
            ValidateLogin(login);

            return TokenGenarationAlgorythm?.Invoke(login.Payload);
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
