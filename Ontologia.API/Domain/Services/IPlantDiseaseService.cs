using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Persistence
{
    public interface IPlantDiseaseService
    {
        // General Methods
        Task<PlantDiseaseResponse> SaveAsync(PlantDisease plantDisease);
        Task<IEnumerable<PlantDisease>> ListAsync();
        Task<PlantDiseaseResponse> GetById(Guid plantDiseaseId);
        Task<PlantDiseaseResponse> Update(Guid plantDiseaseId, PlantDisease plantDisease);
        Task<PlantDiseaseResponse> Delete(Guid plantDiseaseId);

        // Methods for CategoryDisease Entity
        Task<IEnumerable<PlantDisease>> ListByUserId(Guid categoryDiseaseId);
        Task<PlantDiseaseResponse> AssingPlantDiseaseToCategoryDisease(Guid categoryDiseaseId, Guid plantDiseaseId);
        Task<PlantDiseaseResponse> UnassingPlantDiseaseToCategoryDisease(Guid categoryDiseaseId, Guid plantDiseaseId);
    }
}
