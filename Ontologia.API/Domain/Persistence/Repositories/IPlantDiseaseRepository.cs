using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IPlantDiseaseRepository
    {
        // General Methods
        Task AddAsync(PlantDisease plantDisease);
        Task<IEnumerable<PlantDisease>> ListAsync();
        Task<PlantDisease> GetById(Guid plantDiseasetId);
        void Update(PlantDisease plantDisease);
        void Remove(PlantDisease plantDisease);

        // Methods for CategoryDisease Entity
        Task<IEnumerable<PlantDisease>> ListByCategoryDiseaseIdAsync(Guid categoryDiseaseId);
        Task AssingPlantDiseaseToCategoryDisease(Guid categoryDiseaseId, Guid plantDiseaseId);
        Task UnassingPlantDiseaseToCategoryDisease(Guid categoryDiseaseId, Guid plantDiseaseId);

    }
}
