using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task AddAsync(User user);
        Task<User> FindById(int id);
        void Update(User user);
        void Remove(User user);
    }
}
