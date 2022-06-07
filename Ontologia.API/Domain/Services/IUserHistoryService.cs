using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserHistoryService
    {
        Task<IEnumerable<UserHistory>> ListAsync();
        Task<UserHistoryResponse> SaveAsync(UserHistory userHistory);
        Task<IEnumerable<UserHistory>> ListByUserId(Guid userId);
        Task<UserHistoryResponse> GetById(Guid userHistoryId);
        Task<UserHistoryResponse> Update(Guid userHistoryId, UserHistory userHistory);
        Task<UserHistoryResponse> Delete(Guid userHistoryId);
        Task<UserHistoryResponse> AssignUserHistory(Guid userId, Guid userHistoryId);
        Task<UserHistoryResponse> UnassignUserHistory(Guid userId, Guid userHistoryId);
    }
}
