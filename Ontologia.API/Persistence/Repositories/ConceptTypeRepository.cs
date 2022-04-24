using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class ConceptTypeRepository : BaseRepository, IConceptTypeRepository
    {
        public ConceptTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ConceptType conceptType)
        {
            conceptType.IsActive = true;
            await _context.ConceptTypes.AddAsync(conceptType);
        }

        public async Task<ConceptType> FindById(Guid id)
        {
            return await _context.ConceptTypes.FindAsync(id);
        }

        public async Task<IEnumerable<ConceptType>> ListAsync()
        {
            return await _context.ConceptTypes.ToListAsync();
        }

        public void Remove(ConceptType conceptType)
        {
            conceptType.IsActive = false;
            _context.ConceptTypes.Remove(conceptType);
        }

        public void Update(ConceptType conceptType)
        {
            _context.ConceptTypes.Update(conceptType);
        }
    }
}
