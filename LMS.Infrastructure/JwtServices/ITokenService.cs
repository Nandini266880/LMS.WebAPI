using LMS.Application.DTOs;
using LMS.Domain.Enums;
using System.Security.Claims;

namespace LMS.Infrastructure.JwtServices
{
    public interface ITokenService
    {
        string GenerateToken(int id, string email, UserRole role, string secretKey, int expiryMinutes);
        string GenerateRefreshToken();
    }
}
