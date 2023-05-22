using System.Security.Cryptography;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Infrastructure.Authentication;

public class TokenGenerator: ITokenGenerator
{
    public string GenerateToken(ulong userId)
    {
        var plainTextToken = Guid.NewGuid().ToString();
        
        var token = Convert.ToBase64String(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(plainTextToken)
            )
        );

        return plainTextToken;
    }
}