using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.Common.Interfaces.Services
{
    public interface IJwtGenerator
    {
        Jwt GenerateToken(User user);
    }
}