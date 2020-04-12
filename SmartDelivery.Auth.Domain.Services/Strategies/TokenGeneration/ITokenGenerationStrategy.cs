using SmartDelivery.Auth.Domain.Model;

namespace SmartDelivery.Auth.Domain.Services.Strategies.TokenGeneration
{
    public interface ITokenGeneratorStrategy
    {
        string GenerateToken(Payload payload);
        Payload GetPayloadByToken(string token);
    }
}