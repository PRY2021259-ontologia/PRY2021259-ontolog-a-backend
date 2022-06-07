using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class CategoryDiseaseRepository : BaseRepository, ICategoryDiseaseRepository
    {
        public CategoryDiseaseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(CategoryDisease categoryDisease)
        {
            categoryDisease.IsActive = true;
            await _context.CategoryDiseases.AddAsync(categoryDisease);
        }

        public async Task<CategoryDisease> FindById(Guid id)
        {
            return await _context.CategoryDiseases.FindAsync(id);
        }

        public async Task<IEnumerable<CategoryDisease>> ListAsync()
        {
            return await _context.CategoryDiseases.ToListAsync();
        }

        public void Remove(CategoryDisease categoryDisease)
        {
            categoryDisease.IsActive = false;
            _context.CategoryDiseases.Remove(categoryDisease);
        }

        public void Update(CategoryDisease categoryDisease)
        {
            _context.CategoryDiseases.Update(categoryDisease);
        }
    }
}
