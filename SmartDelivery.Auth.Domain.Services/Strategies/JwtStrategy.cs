using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SmartDelivery.Auth.Domain.Model;

namespace SmartDelivery.Auth.Domain.Services.Strategies
{
    public class JwtStrategy
    {
        public static string GenerateToken(Payload payload) 
        {
            var encodedSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes("sdhsaiuysf9doasjoshfuisdhfidfsdf9sd8fyhsfisdfguydfts7d6937"));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, payload.Email),
                new Claim("sub", payload.Sub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secret = Convert.FromBase64String(encodedSecret);

            var tokenConfig = new JwtSecurityToken(
               issuer: payload.Iss, //TODO: add to config file
               audience: null,
               claims: claims,
               expires: payload.Exp,
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(tokenConfig);
        }
    }
}