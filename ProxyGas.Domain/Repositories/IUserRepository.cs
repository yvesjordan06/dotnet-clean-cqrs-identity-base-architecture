using ProxyGas.Domain.Entities;

namespace ProxyGas.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task<User> GetById(Guid id);
    Task<User> Create(User user);
    Task<User> Update(User user);
    void Delete(User user);
}