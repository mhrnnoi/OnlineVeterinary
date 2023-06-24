using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository 
    : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);

       
    }
}