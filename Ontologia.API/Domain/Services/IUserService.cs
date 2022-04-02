using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(Guid id, User user);
        Task<UserResponse> DeleteAsync(Guid id);
    }
}
