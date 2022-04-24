using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;

namespace Ontologia.API.Persistence.Repositories
{
    public class StatusTypeRepository : BaseRepository, IStatusTypeRepository
    {
        public StatusTypeRepository(AppDbContext context) : base(context)
        {
        }

        // General Methods
        public async Task AddAsync(StatusType statusType)
        {
            statusType.IsActive = true;
            await _context.StatusTypes.AddAsync(statusType);
        }

        public async Task<StatusType> FindById(Guid id)
        {
            return await _context.StatusTypes.FindAsync(id);
        }

        public async Task<IEnumerable<StatusType>> ListAsync()
        {
            return await _context.StatusTypes.ToListAsync();
        }

        public void Remove(StatusType statusType)
        {
            statusType.IsActive = false;
            _context.StatusTypes.Remove(statusType);
        }

        public void Update(StatusType statusType)
        {
            _context.StatusTypes.Update(statusType);
        }
    }
}
