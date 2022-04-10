using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services
{
    public interface IUserConceptPlantDiseaseService
    {
        Task<IEnumerable<UserConceptPlantDisease>> ListAsync();
        Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAsync(Guid userConceptId);
        Task<IEnumerable<UserConceptPlantDisease>> ListByPlantDiseaseIdAsync(Guid plantDiseaseId);
        Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAndPlantDiseaseIdAsync(Guid userConceptId, Guid plantDiseaseId);
        Task<UserConceptPlantDisease> AssignUserConceptPlantDiseaseAsync(Guid userConceptId, Guid plantDiseaseId);
        Task<UserConceptPlantDisease> UnassignUserConceptPlantDiseaseAsync(Guid userConceptId, Guid plantDiseaseId);
    }
}
