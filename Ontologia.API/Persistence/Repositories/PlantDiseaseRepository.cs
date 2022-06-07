using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class PlantDiseaseRepository : BaseRepository, IPlantDiseaseRepository
    {
        public PlantDiseaseRepository(AppDbContext context) : base(context)
        {
        }

        // General Methods
        public async Task AddAsync(PlantDisease plantDisease)
        {
            plantDisease.IsActive = true;
            await _context.PlantDiseases.AddAsync(plantDisease);
        }

        public async Task<IEnumerable<PlantDisease>> ListAsync()
        {
            return await _context.PlantDiseases.ToListAsync();
        }

        public async Task<PlantDisease> GetById(Guid plantDiseaseId)
        {
            return await _context.PlantDiseases.FindAsync(plantDiseaseId);
        }
        public async Task<PlantDisease?> GetByOntologyId(string ontologyId)
        {
            return await _context.PlantDiseases.FirstOrDefaultAsync(x=> x.OntologyId == ontologyId && x.IsActive);
        }
        public void Update(PlantDisease plantDisease)
        {
            _context.PlantDiseases.Update(plantDisease);
        }

        public void Remove(PlantDisease plantDisease)
        {
            plantDisease.IsActive = false;
            _context.PlantDiseases.Remove(plantDisease);
        }

        // Methods for CategoryDisease Entity
        public async Task<IEnumerable<PlantDisease>> ListByCategoryDiseaseIdAsync(long categoryDiseaseId)
        {
            return await _context.PlantDiseases.Where(pD => pD.CategoryDiseaseId == categoryDiseaseId).ToListAsync();
        }

        public async Task AssingPlantDiseaseToCategoryDisease(long categoryDiseaseId, Guid plantDiseaseId)
        {
            CategoryDisease categoryDisease = await _context.CategoryDiseases.FindAsync(categoryDiseaseId);
            PlantDisease plantDisease = await _context.PlantDiseases.FindAsync(plantDiseaseId);

            if (categoryDisease != null && plantDisease != null)
            {
                plantDisease.CategoryDiseaseId = categoryDiseaseId;
                Update(plantDisease);
            }
        }

        public async Task UnassingPlantDiseaseToCategoryDisease(long categoryDiseaseId, Guid plantDiseaseId)
        {
            CategoryDisease categoryDisease = await _context.CategoryDiseases.FindAsync(categoryDiseaseId);
            PlantDisease plantDisease = await _context.PlantDiseases.FindAsync(plantDiseaseId);
            var newId = default(long);

            if (categoryDisease != null && plantDisease != null)
            {
                plantDisease.CategoryDiseaseId = newId;
                Update(plantDisease);
            }
        }


    }
}
