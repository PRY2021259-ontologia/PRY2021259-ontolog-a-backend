using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IStatusTypeRepository
    {
        // General Methods
        Task<IEnumerable<StatusType>> ListAsync();
        Task AddAsync(StatusType statusType);
        Task<StatusType> FindById(Guid id);
        void Update(StatusType statusType);
        void Remove(StatusType statusType);
    }
}
