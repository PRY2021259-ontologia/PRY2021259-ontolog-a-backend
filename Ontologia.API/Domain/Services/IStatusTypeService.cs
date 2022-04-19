using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IStatusTypeService
    {
        // General Methods
        Task<IEnumerable<StatusType>> ListAsync();
        Task<StatusTypeResponse> GetByIdAsync(Guid id);
        Task<StatusTypeResponse> SaveAsync(StatusType statusType);
        Task<StatusTypeResponse> UpdateAsync(Guid id, StatusType statusType);
        Task<StatusTypeResponse> DeleteAsync(Guid id);
    }
}
