using System;
using System.Text;
using SmartDelivery.Auth.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace SmartDelivery.Auth.Domain.Services
{
    public class LoginService
    {
        //TODO: refactor and separate into a strategy class
        private Func<Payload,string> GenarationAlgorythm = (Payload pay) => {

            var encodedSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes("sdhsaiuysf9doasjoshfuisdhfidfsdf9sd8fyhsfisdfguydfts7d6937"));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, pay.Email),
                new Claim("sub", pay.Sub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secret = Convert.FromBase64String(encodedSecret);

            var tokenConfig = new JwtSecurityToken(
               issuer: pay.Iss, //TODO: add to config file 
               audience: null,
               claims: claims,
               expires: pay.Exp,
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(tokenConfig);

            return token;
        };

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
