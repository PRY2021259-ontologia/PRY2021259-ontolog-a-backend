using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserConceptPlantDiseaseRepository
    {
        Task<IEnumerable<UserConceptPlantDisease>> ListAsync();
        Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAsync(Guid userConceptId);
        Task<IEnumerable<UserConceptPlantDisease>> ListByPlantDiseaseIdAsync(Guid plantDiseaseId);
        Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAndPlantDiseaseIdAsync(Guid userConceptId, Guid plantDiseaseId);
        Task<UserConceptPlantDisease> FindByUserConceptIdAndPlantDiseaseId(Guid userConceptId, Guid plantDiseaseId);
        Task AddAsync(UserConceptPlantDisease userConceptPlantDisease);
        void Remove(UserConceptPlantDisease userConceptPlantDisease);
        Task AssignUserConceptPlantDisease(Guid userConceptId, Guid plantDiseaseId);
        Task UnassignUserConceptPlantDisease(Guid userConceptId, Guid plantDiseaseId);
    }
}
