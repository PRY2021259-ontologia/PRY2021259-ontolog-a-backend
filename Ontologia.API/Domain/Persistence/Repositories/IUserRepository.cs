using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserRepository
    {
        // General Methods
        Task<IEnumerable<User>> ListAsync();
        Task AddAsync(User user);
        Task<User> FindById(Guid id);
        void Update(User user);
        void Remove(User user);


        // Methods for UserType Entity
        Task<IEnumerable<User>> ListByUserTypeIdAsync(Guid userTypeId);
        Task AssingUserToUserType(Guid userTypeId, Guid userId);
        Task UnassingUserToUserType(Guid userTypeId, Guid userId);
    }
}
