using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface ICategoryDiseaseService
    {
        Task<IEnumerable<CategoryDisease>> ListAsync();
        Task<CategoryDiseaseResponse> GetByIdAsync(Guid id);
        Task<CategoryDiseaseResponse> SaveAsync(CategoryDisease categoryDisease);
        Task<CategoryDiseaseResponse> UpdateAsync(Guid id, CategoryDisease categoryDisease);
        Task<CategoryDiseaseResponse> DeleteAsync(Guid id);
    }
}
