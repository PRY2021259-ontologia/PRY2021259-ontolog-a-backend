using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserTypeService
    {
        Task<IEnumerable<UserType>> ListAsync();
        Task<UserTypeResponse> GetByIdAsync(Guid id);
        Task<UserTypeResponse> SaveAsync(UserType userType);
        Task<UserTypeResponse> UpdateAsync(Guid id, UserType userType);
        Task<UserTypeResponse> DeleteAsync(Guid id);
    }
}
