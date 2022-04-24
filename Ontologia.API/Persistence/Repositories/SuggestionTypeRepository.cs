using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class SuggestionTypeRepository : BaseRepository, ISuggestionTypeRepository
    {
        public SuggestionTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SuggestionType suggestionType)
        {
            suggestionType.IsActive = true;
            await _context.SuggestionTypes.AddAsync(suggestionType);
        }

        public async Task<SuggestionType> FindById(Guid id)
        {
            return await _context.SuggestionTypes.FindAsync(id);
        }

        public async Task<IEnumerable<SuggestionType>> ListAsync()
        {
            return await _context.SuggestionTypes.ToListAsync();
        }

        public void Remove(SuggestionType suggestionType)
        {
            suggestionType.IsActive = false;
            _context.SuggestionTypes.Remove(suggestionType);
        }

        public void Update(SuggestionType suggestionType)
        {
            _context.SuggestionTypes.Update(suggestionType);
        }
    }
}
