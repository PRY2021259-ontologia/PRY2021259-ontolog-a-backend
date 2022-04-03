using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserService
    {
        // General Methods
        Task<IEnumerable<User>> ListAsync();
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(Guid id, User user);
        Task<UserResponse> DeleteAsync(Guid id);

        // Methods for UserType Entity
        Task<IEnumerable<User>> ListByUserTypeId(Guid userTypeId);
        Task<UserResponse> AssignUserToUserType(Guid userTypeId, Guid userId);
        Task<UserResponse> UnassignUserToUserType(Guid userTypeId, Guid userId);
    }
}
