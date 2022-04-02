using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserHistoryRepository
    {
        Task<IEnumerable<UserHistory>> ListAsync();
        Task AddAsync(UserHistory userHistory);
        Task<IEnumerable<UserHistory>> ListByUserIdAsync(Guid userId);
        Task<UserHistory> GetById(Guid userHistoryId);
        Task AssingUserHistory(Guid userId, Guid userHistoryId);
        Task UnassingUserHistory(Guid userId, Guid userHistoryId);
        void Update(UserHistory userHistory);
        void Remove(UserHistory userHistory);
    }
}
