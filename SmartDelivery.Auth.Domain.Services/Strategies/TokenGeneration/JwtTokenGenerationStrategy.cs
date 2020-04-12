using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SmartDelivery.Auth.Domain.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SmartDelivery.Auth.Domain.Services.Strategies.TokenGeneration
{
    public class JwtTokenGeneratorStrategy : ITokenGeneratorStrategy
    {
        private string _secret;

        public JwtTokenGeneratorStrategy(string secret)
        {
            _secret = secret;
        }

        public string GenerateToken(Payload payload) 
        {
            var encodedSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(_secret));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, payload.Email),
                new Claim("sub", payload.Sub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secret = Convert.FromBase64String(encodedSecret);

            var tokenConfig = new JwtSecurityToken(
               audience: null,
               claims: claims,
               expires: payload.Exp,
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(tokenConfig);
        }

        public Payload GetPayloadByToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_secret);
            var handler = new JwtSecurityTokenHandler();

            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            var tok = (JwtSecurityToken)tokenSecure;

            return new Payload(tok.Payload["sub"].ToString(), DateTime.Now, tok.Payload["unique_name"].ToString());
        }
    }
}
