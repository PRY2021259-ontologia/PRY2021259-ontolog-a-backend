using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class SuggestionStatusRepository : BaseRepository, ISuggestionStatusRepository
    {
        public SuggestionStatusRepository(AppDbContext context) : base(context)
        {
        }

        // General Methods
        public async Task AddAsync(SuggestionStatus suggestionStatus)
        {
            suggestionStatus.IsActive = true;
            await _context.SuggestionStatuses.AddAsync(suggestionStatus);
        }

        public async Task<IEnumerable<SuggestionStatus>> ListAsync()
        {
            return await _context.SuggestionStatuses.ToListAsync();
        }

        public async Task<SuggestionStatus> GetById(Guid suggestionStatusId)
        {
            return await _context.SuggestionStatuses.FindAsync(suggestionStatusId);
        }

        public void Update(SuggestionStatus suggestionStatus)
        {
            _context.SuggestionStatuses.Update(suggestionStatus);
        }

        public void Remove(SuggestionStatus suggestionStatus)
        {
            suggestionStatus.IsActive = false;
            _context.SuggestionStatuses.Remove(suggestionStatus);
        }


        // Methods for StatusType Entity
        public async Task<IEnumerable<SuggestionStatus>> ListByStatusTypeIdAsync(Guid statusTypeId)
        {
            return await _context.SuggestionStatuses.Where(pD => pD.StatusTypeId == statusTypeId).ToListAsync();
        }

        public async Task AssingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId)
        {
            StatusType statusType = await _context.StatusTypes.FindAsync(statusTypeId);
            SuggestionStatus suggestionStatus = await _context.SuggestionStatuses.FindAsync(suggestionStatusId);

            if (statusType != null && suggestionStatus != null)
            {
                suggestionStatus.StatusTypeId = statusTypeId;
                Update(suggestionStatus);
            }
        }

        public async Task UnassingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId)
        {
            StatusType statusType = await _context.StatusTypes.FindAsync(statusTypeId);
            SuggestionStatus suggestionStatus = await _context.SuggestionStatuses.FindAsync(suggestionStatusId);
            var newId = Guid.Empty;

            if (statusType != null && suggestionStatus != null)
            {
                suggestionStatus.StatusTypeId = newId;
                Update(suggestionStatus);
            }
        }

        // Methods for UserSuggestion Entity
        public async Task<IEnumerable<SuggestionStatus>> ListByUserSuggestionIdAsync(Guid userSuggestionId)
        {
            return await _context.SuggestionStatuses.Where(pD => pD.UserSuggestionId == userSuggestionId).ToListAsync();
        }

        public async Task AssingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId)
        {
            UserSuggestion userSuggestion = await _context.UserSuggestions.FindAsync(userSuggestionId);
            SuggestionStatus suggestionStatus = await _context.SuggestionStatuses.FindAsync(suggestionStatusId);

            if (userSuggestion != null && suggestionStatus != null)
            {
                suggestionStatus.UserSuggestionId = userSuggestionId;
                Update(suggestionStatus);
            }
        }

        public async Task UnassingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId)
        {
            UserSuggestion userSuggestion = await _context.UserSuggestions.FindAsync(userSuggestionId);
            SuggestionStatus suggestionStatus = await _context.SuggestionStatuses.FindAsync(suggestionStatusId);
            var newId = Guid.Empty;

            if (userSuggestion != null && suggestionStatus != null)
            {
                suggestionStatus.UserSuggestionId = newId;
                Update(suggestionStatus);
            }
        }
    }
}
