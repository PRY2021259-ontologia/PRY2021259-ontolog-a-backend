using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ontologia.API.Persistence.Repositories
{
    public class UserConceptPlantDiseaseRepository : BaseRepository, IUserConceptPlantDiseaseRepository
    {
        public UserConceptPlantDiseaseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(UserConceptPlantDisease userConceptPlantDisease)
        {
            await _context.UserConceptPlantDiseases.AddAsync(userConceptPlantDisease);
        }

        public async Task AssignUserConceptPlantDisease(Guid userConceptId, Guid plantDiseaseId)
        {
            UserConceptPlantDisease userConceptPlantDisease = await FindByUserConceptIdAndPlantDiseaseId(userConceptId, plantDiseaseId);

            if (userConceptPlantDisease == null)
            {
                userConceptPlantDisease = new UserConceptPlantDisease { UserConceptId = userConceptId, PlantDiseaseId = plantDiseaseId };
                await AddAsync(userConceptPlantDisease);
            }

        }

        public async Task<UserConceptPlantDisease> FindByUserConceptIdAndPlantDiseaseId(Guid userConceptId, Guid plantDiseaseId)
        {
            return await _context.UserConceptPlantDiseases.FindAsync(userConceptId, plantDiseaseId);
        }

        public async Task<IEnumerable<UserConceptPlantDisease>> ListAsync()
        {
            return await _context.UserConceptPlantDiseases
              .Include(pt => pt.UserConcept)
              .Include(pt => pt.PlantDisease)
              .ToListAsync();
        }

        public async Task<IEnumerable<UserConceptPlantDisease>> ListByPlantDiseaseIdAsync(Guid plantDiseaseId)
        {
            return await _context.UserConceptPlantDiseases
             .Where(pt => pt.PlantDiseaseId == plantDiseaseId)
             .Include(pt => pt.UserConcept)
             .Include(pt => pt.PlantDisease)
             .ToListAsync();
        }

        public async Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAndPlantDiseaseIdAsync(Guid userConceptId, Guid plantDiseaseId)
        {
            return await _context.UserConceptPlantDiseases
             .Where(pt => pt.UserConceptId == userConceptId)
             .Where(pt => pt.PlantDiseaseId == plantDiseaseId)
             .Include(pt => pt.UserConcept)
             .Include(pt => pt.PlantDisease)
             .ToListAsync();
        }

        public async Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAsync(Guid userConceptId)
        {
            return await _context.UserConceptPlantDiseases
              .Where(pt => pt.UserConceptId == userConceptId)
              .Include(pt => pt.UserConcept)
              .Include(pt => pt.PlantDisease)
              .ToListAsync();
        }

        public void Remove(UserConceptPlantDisease userConceptPlantDisease)
        {
            _context.UserConceptPlantDiseases.Remove(userConceptPlantDisease);
        }

        public async Task UnassignUserConceptPlantDisease(Guid userConceptId, Guid plantDiseaseId)
        {
            UserConceptPlantDisease userConceptPlantDisease = await FindByUserConceptIdAndPlantDiseaseId(userConceptId, plantDiseaseId);
            if (userConceptPlantDisease != null)
                Remove(userConceptPlantDisease);
        }
    }
}
