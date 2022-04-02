using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserConceptRepository
    {
        Task<IEnumerable<UserConcept>> ListAsync();
        Task AddAsync(UserConcept userConcept);
        Task<IEnumerable<UserConcept>> ListByUserIdAsync(Guid userId);
        Task<UserConcept> GetById(Guid userConceptId);
        Task AssingUserConcept(Guid userId, Guid userConceptId);
        Task UnassingUserConcept(Guid userId, Guid userConceptId);
        void Update(UserConcept userConcept);
        void Remove(UserConcept userConcept);
    }
}
