using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserTypeRepository
    {
        Task<IEnumerable<UserType>> ListAsync();
        Task AddAsync(UserType userType);
        Task<UserType> FindById(Guid id);
        void Update(UserType userType);
        void Remove(UserType userType);
    }
}
