using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserConceptRepository
    {
        // General Methods
        Task AddAsync(UserConcept userConcept);
        Task<IEnumerable<UserConcept>> ListAsync();
        Task<UserConcept> GetById(Guid userConceptId);
        void Update(UserConcept userConcept);
        void Remove(UserConcept userConcept);

        // Methods for User Entity
        Task<IEnumerable<UserConcept>> ListByUserIdAsync(Guid userId);
        Task AssingUserConceptToUser(Guid userId, Guid userConceptId);
        Task UnassingUserConceptToUser(Guid userId, Guid userConceptId);

        // Methods for ConceptType Entity
        Task<IEnumerable<UserConcept>> ListByConceptTypeIdAsync(Guid conceptTypeId);
        Task AssingUserConceptToConceptType(Guid conceptTypeId, Guid userConceptId);
        Task UnassingUserConceptToConceptType(Guid conceptTypeId, Guid userConceptId);
    }
}
