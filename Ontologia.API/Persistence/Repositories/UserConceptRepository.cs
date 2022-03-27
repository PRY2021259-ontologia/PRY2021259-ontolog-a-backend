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

        public async Task AddAsync(UserConcept userConcept)
        {
            await _context.UserConcepts.AddAsync(userConcept);
        }

        public async Task AssingUserConcept(int userId, int userConceptId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserConcept userConcept= await _context.UserConcepts.FindAsync(userConceptId);

            if (user != null && userConcept != null)
            {
                userConcept.UserId = userId ;
                Update(userConcept);
            }
        }

        public async Task<UserConcept> GetById(int userConceptId)
        {
            return await _context.UserConcepts.FindAsync(userConceptId);
        }

        public async Task<IEnumerable<UserConcept>> ListAsync()
        {
            return await _context.UserConcepts.ToListAsync();
        }

        public async Task<IEnumerable<UserConcept>> ListByUserIdAsync(int userId)
        {
            return await _context.UserConcepts.Where(uC => uC.UserId == userId).ToListAsync();
        }

        public void Remove(UserConcept userConcept)
        {
            _context.UserConcepts.Remove(userConcept);
        }

        public async Task UnassingUserConcept(int userId, int userConceptId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserConcept userConcept = await _context.UserConcepts.FindAsync(userConceptId);

            if (user != null && userConcept != null)
            {
                userConcept.UserId = 0;
                Update(userConcept);
            }
        }

        public void Update(UserConcept userConcept)
        {
            _context.UserConcepts.Update(userConcept);
        }
    }
}
