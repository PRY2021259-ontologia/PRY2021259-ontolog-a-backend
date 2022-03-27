using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserConceptRepository
    {
        Task<IEnumerable<UserConcept>> ListAsync();
        Task AddAsync(UserConcept userConcept);
        Task<IEnumerable<UserConcept>> ListByUserIdAsync(int userId);
        Task<UserConcept> GetById(int userConceptId);
        Task AssingUserConcept(int userId, int userConceptId);
        Task UnassingUserConcept(int userId, int userConceptId);
        void Update(UserConcept userConcept);
        void Remove(UserConcept userConcept);
    }
}
