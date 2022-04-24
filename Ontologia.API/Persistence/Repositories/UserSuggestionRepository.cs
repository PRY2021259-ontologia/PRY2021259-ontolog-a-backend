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

        // General Methods

        public async Task<UserSuggestion> GetById(Guid userSuggestionId)
        {
            return await _context.UserSuggestions.FindAsync(userSuggestionId);
        }

        public async Task<IEnumerable<UserSuggestion>> ListAsync()
        {
            return await _context.UserSuggestions.ToListAsync();
        }

        public async Task AddAsync(UserSuggestion userSuggestion)
        {
            userSuggestion.IsActive = true;
            await _context.UserSuggestions.AddAsync(userSuggestion);
        }

        public void Update(UserSuggestion userSuggestion)
        {
            _context.UserSuggestions.Update(userSuggestion);
        }

        public void Remove(UserSuggestion userSuggestion)
        {
            userSuggestion.IsActive = false;
            _context.UserSuggestions.Remove(userSuggestion);
        }


        // Methods for User Entity


        public async Task<IEnumerable<UserSuggestion>> ListByUserIdAsync(Guid userId)
        {
            return await _context.UserSuggestions.Where(uS => uS.UserId == userId).ToListAsync();
        }

        public async Task AssingUserSuggestion(Guid userId, Guid userSuggestionId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserSuggestion userSuggestion = await _context.UserSuggestions.FindAsync(userSuggestionId);

            if (user != null && userSuggestion != null)
            {
                userSuggestion.UserId = userId;
                Update(userSuggestion);
            }
        }

        public async Task UnassingUserSuggestion(Guid userId, Guid userSuggestionId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserSuggestion userSuggestion= await _context.UserSuggestions.FindAsync(userSuggestionId);

            if (user != null && userSuggestion != null)
            {
                userSuggestion.UserId = userId;
                Update(userSuggestion);
            }
        }

        // Methods for SuggestionType Entity
        public async Task<IEnumerable<UserSuggestion>> ListBySuggestionTypeIdAsync(Guid SuggestionTypeId)
        {
            return await _context.UserSuggestions.Where(uC => uC.SuggestionTypeId == SuggestionTypeId).ToListAsync();
        }

        public async Task AssingUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId)
        {
            SuggestionType suggestionType = await _context.SuggestionTypes.FindAsync(suggestionTypeId);
            UserSuggestion userSuggestion = await _context.UserSuggestions.FindAsync(userSuggestionId);

            if (suggestionType != null && userSuggestion != null)
            {
                userSuggestion.SuggestionTypeId = suggestionTypeId;
                Update(userSuggestion);
            }
        }

        public async Task UnassingUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId)
        {
            SuggestionType suggestionType = await _context.SuggestionTypes.FindAsync(suggestionTypeId);
            UserSuggestion userSuggestion = await _context.UserSuggestions.FindAsync(userSuggestionId);
            var newId = Guid.Empty;

            if (suggestionType != null && userSuggestion != null)
            {
                userSuggestion.SuggestionTypeId = newId;
                Update(userSuggestion);
            }
        }


    }
}
