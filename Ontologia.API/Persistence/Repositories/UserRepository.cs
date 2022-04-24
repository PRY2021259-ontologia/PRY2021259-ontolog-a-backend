using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        // General Methods
        public async Task AddAsync(User user)
        {
            user.IsActive = true;
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public void Remove(User user)
        {
            user.IsActive = false;
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        // Methods for UserType Entity

        public async Task<IEnumerable<User>> ListByUserTypeIdAsync(Guid userTypeId)
        {
            return await _context.Users.Where(u => u.UserTypeId == userTypeId).ToListAsync();
        }


        public async Task AssingUserToUserType(Guid userTypeId, Guid userId)
        {
            UserType userType = await _context.UserTypes.FindAsync(userTypeId);
            User user = await _context.Users.FindAsync(userId);

            if (userType != null && user != null)
            {
                user.UserTypeId = userTypeId;
                Update(user);
            }
        }

        public async Task UnassingUserToUserType(Guid userTypeId, Guid userId)
        {
            UserType userType = await _context.UserTypes.FindAsync(userTypeId);
            User user = await _context.Users.FindAsync(userId);
            var newId = Guid.Empty;

            if (userType != null && user != null)
            {
                user.UserTypeId = newId;
                Update(user);
            }
        }

    }
}
