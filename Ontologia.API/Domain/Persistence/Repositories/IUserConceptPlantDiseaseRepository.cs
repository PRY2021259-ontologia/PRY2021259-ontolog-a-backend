using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserConceptPlantDiseaseRepository
    {
        Task<IEnumerable<UserConceptPlantDisease>> ListAsync();
        Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAsync(Guid userConceptId);
        Task<IEnumerable<UserConceptPlantDisease>> ListByPlantDiseaseIdAsync(Guid plantDiseaseId);
        Task<IEnumerable<UserConceptPlantDisease>> ListByUserIdAndTutorIdAsync(Guid userId, Guid tutorId);
        Task<UserSession> FindByUserIdAndSessionId(Guid userId, Guid sessionId);
        Task AddAsync(UserSession userSession);
        void Remove(UserSession userSession);
        Task AssignUserSession(Guid userId, Guid sessionId);
        Task UnassignUserSession(Guid userId, Guid sessionId);
    }
}
