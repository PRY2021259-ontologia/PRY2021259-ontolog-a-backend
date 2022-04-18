using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserLoginRepository
    {
        // General Methods
        Task<IEnumerable<UserLogin>> ListAsync();
        Task AddAsync(UserLogin userLogin);
        Task<UserLogin> FindById(Guid id);
        void Update(UserLogin userLogin);
        void Remove(UserLogin userLogin);
    }
}
