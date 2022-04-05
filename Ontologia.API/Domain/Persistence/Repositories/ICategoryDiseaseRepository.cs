using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface ICategoryDiseaseRepository
    {
        Task<IEnumerable<CategoryDisease>> ListAsync();
        Task AddAsync(CategoryDisease categoryDisease);
        Task<CategoryDisease> FindById(Guid id);
        void Update(CategoryDisease categoryDisease);
        void Remove(CategoryDisease categoryDisease);
    }
}
