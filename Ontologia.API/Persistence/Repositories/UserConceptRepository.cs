using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class UserConceptRepository : BaseRepository, IUserConceptRepository
    {
        public UserConceptRepository(AppDbContext context) : base(context)
        {
        }

        // General Methods

        public async Task AddAsync(UserConcept userConcept)
        {
            userConcept.IsActive = true;
            await _context.UserConcepts.AddAsync(userConcept);
        }

        public async Task<IEnumerable<UserConcept>> ListAsync()
        {
            return await _context.UserConcepts.ToListAsync();
        }

        public async Task<UserConcept> GetById(Guid userConceptId)
        {
            return await _context.UserConcepts.FindAsync(userConceptId);
        }

        public void Update(UserConcept userConcept)
        {
            _context.UserConcepts.Update(userConcept);
        }

        public void Remove(UserConcept userConcept)
        {
            userConcept.IsActive = false;
            _context.UserConcepts.Remove(userConcept);
        }

        // Methods for User Entity

        public async Task<IEnumerable<UserConcept>> ListByUserIdAsync(Guid userId)
        {
            return await _context.UserConcepts.Where(uC => uC.UserId == userId).ToListAsync();
        }

        public async Task AssingUserConceptToUser(Guid userId, Guid userConceptId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserConcept userConcept= await _context.UserConcepts.FindAsync(userConceptId);

            if (user != null && userConcept != null)
            {
                userConcept.UserId = userId;
                Update(userConcept);
            }
        }

        public async Task UnassingUserConceptToUser(Guid userId, Guid userConceptId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserConcept userConcept = await _context.UserConcepts.FindAsync(userConceptId);
            var newId = Guid.Empty;

            if (user != null && userConcept != null)
            {
                userConcept.UserId = newId;
                Update(userConcept);
            }
        }

        // Methods for ConceptType Entity

        public async Task<IEnumerable<UserConcept>> ListByConceptTypeIdAsync(Guid conceptTypeId)
        {
            return await _context.UserConcepts.Where(uC => uC.ConceptTypeId == conceptTypeId).ToListAsync();
        }

        public async Task AssingUserConceptToConceptType(Guid conceptTypeId, Guid userConceptId)
        {
            ConceptType conceptType = await _context.ConceptTypes.FindAsync(conceptTypeId);
            UserConcept userConcept = await _context.UserConcepts.FindAsync(userConceptId);

            if (conceptType != null && userConcept != null)
            {
                userConcept.ConceptTypeId = conceptTypeId;
                Update(userConcept);
            }
        }

        public async Task UnassingUserConceptToConceptType(Guid conceptTypeId, Guid userConceptId)
        {
            ConceptType conceptType = await _context.ConceptTypes.FindAsync(conceptTypeId);
            UserConcept userConcept = await _context.UserConcepts.FindAsync(userConceptId);
            var newId = Guid.Empty;

            if (conceptType != null && userConcept != null)
            {
                userConcept.ConceptTypeId = newId;
                Update(userConcept);
            }
        }

    }
}
