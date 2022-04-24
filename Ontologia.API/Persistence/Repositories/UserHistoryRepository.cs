using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class UserHistoryRepository : BaseRepository, IUserHistoryRepository
    {
        public UserHistoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(UserHistory userHistory)
        {
            userHistory.IsActive = true;
            await _context.UserHistories.AddAsync(userHistory);
        }

        public async Task AssingUserHistory(Guid userId, Guid userHistoryId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserHistory userHistory = await _context.UserHistories.FindAsync(userHistoryId);

            if (user != null && userHistory != null)
            {
                userHistory.UserId = userId;
                Update(userHistory);
            }
        }

        public async Task<UserHistory> GetById(Guid userHistoryId)
        {
            return await _context.UserHistories.FindAsync(userHistoryId);
        }

        public async Task<IEnumerable<UserHistory>> ListAsync()
        {
            return await _context.UserHistories.ToListAsync();
        }

        public async Task<IEnumerable<UserHistory>> ListByUserIdAsync(Guid userId)
        {
            return await _context.UserHistories.Where(uH => uH.UserId == userId).ToListAsync();
        }

        public void Remove(UserHistory userHistory)
        {
            userHistory.IsActive = false;
            _context.UserHistories.Remove(userHistory);
        }

        public async Task UnassingUserHistory(Guid userId, Guid userHistoryId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserHistory userHistory = await _context.UserHistories.FindAsync(userHistoryId);

            if (user != null && userHistory != null)
            {
                userHistory.UserId = userId;
                Update(userHistory);
            }
        }

        public void Update(UserHistory userHistory)
        {
            _context.UserHistories.Update(userHistory);
        }
    }
}
