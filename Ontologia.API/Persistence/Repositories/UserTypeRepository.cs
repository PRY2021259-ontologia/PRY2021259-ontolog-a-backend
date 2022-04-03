using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class UserTypeRepository : BaseRepository, IUserTypeRepository
    {
        public UserTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(UserType userType)
        {
            await _context.UserTypes.AddAsync(userType);
        }

        public async Task<UserType> FindById(Guid id)
        {
            return await _context.UserTypes.FindAsync(id);
        }

        public async Task<IEnumerable<UserType>> ListAsync()
        {
            return await _context.UserTypes.ToListAsync();
        }

        public void Remove(UserType userType)
        {
            _context.UserTypes.Remove(userType);
        }

        public void Update(UserType userType)
        {
            _context.UserTypes.Update(userType);
        }
    }
}
