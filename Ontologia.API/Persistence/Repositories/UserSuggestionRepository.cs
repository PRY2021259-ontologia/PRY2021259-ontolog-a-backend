using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class UserSuggestionRepository : BaseRepository, IUserSuggestionRepository
    {
        public UserSuggestionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(UserSuggestion userSuggestion)
        {
            await _context.UserSuggestions.AddAsync(userSuggestion);
        }

        public async Task AssingUserSuggestion(int userId, int userSuggestionId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserSuggestion userSuggestion = await _context.UserSuggestions.FindAsync(userSuggestionId);

            if (user != null && userSuggestion != null)
            {
                userSuggestion.UserId = userId;
                Update(userSuggestion);
            }
        }

        public async Task<UserSuggestion> GetById(int userSuggestionId)
        {
            return await _context.UserSuggestions.FindAsync(userSuggestionId);
        }

        public async Task<IEnumerable<UserSuggestion>> ListAsync()
        {
            return await _context.UserSuggestions.ToListAsync();
        }

        public async Task<IEnumerable<UserSuggestion>> ListByUserIdAsync(int userId)
        {
            return await _context.UserSuggestions.Where(uS => uS.UserId == userId).ToListAsync();
        }

        public void Remove(UserSuggestion userSuggestion)
        {
            _context.UserSuggestions.Remove(userSuggestion);
        }

        public async Task UnassingUserSuggestion(int userId, int userSuggestionId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserSuggestion userSuggestion= await _context.UserSuggestions.FindAsync(userSuggestionId);

            if (user != null && userSuggestion != null)
            {
                userSuggestion.UserId = 0;
                Update(userSuggestion);
            }
        }

        public void Update(UserSuggestion userSuggestion)
        {
            _context.UserSuggestions.Update(userSuggestion);
        }
    }
}
