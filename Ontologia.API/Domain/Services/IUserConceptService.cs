using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserConceptService
    {
        // General Methods
        Task<UserConceptResponse> SaveAsync(UserConcept userConcept);
        Task<IEnumerable<UserConcept>> ListAsync();
        Task<UserConceptResponse> GetById(Guid userConceptId);
        Task<UserConceptResponse> Update(Guid userConceptId, UserConcept userConcept);
        Task<UserConceptResponse> Delete(Guid userConceptId);

        // Methods for User Entity
        Task<IEnumerable<UserConcept>> ListByUserId(Guid userId);
        Task<UserConceptResponse> AssignUserConceptToUser(Guid userId, Guid userConceptId);
        Task<UserConceptResponse> UnassignUserConceptToUser(Guid userId, Guid userConceptId);

        // Methods for ConceptType Entity
        Task<IEnumerable<UserConcept>> ListByConceptTypeId(Guid conceptTypeId);
        Task<UserConceptResponse> AssignUserConceptToConceptType(Guid conceptTypeId, Guid userConceptId);
        Task<UserConceptResponse> UnassignUserConceptToConceptType(Guid conceptTypeId, Guid userConceptId);

        // Methods for UserConceptPlantDisease Entity
        Task<IEnumerable<UserConcept>> ListByPlantDiseaseIdAsync(Guid plantDiseaseId);
    }
}
