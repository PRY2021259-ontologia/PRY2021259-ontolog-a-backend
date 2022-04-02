using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IConceptTypeRepository
    {
        Task<IEnumerable<ConceptType>> ListAsync();
        Task AddAsync(ConceptType conceptType);
        Task<ConceptType> FindById(Guid id);
        void Update(ConceptType conceptType);
        void Remove(ConceptType conceptType);
    }
}
