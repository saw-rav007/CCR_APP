using CCR.Domain.Entities;

namespace CCR.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task<bool> ExistsAsync(string email);
}
