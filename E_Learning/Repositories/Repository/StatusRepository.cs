using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddStatusAsync(Status status)
        {
            status.NameId = Guid.NewGuid().ToString();  // Ensure Id is set
            await _context.Statuses.AddAsync(status);
            await _context.SaveChangesAsync();
        }

        public async Task<Status> GetStatusByIdAsync(Guid id)
        {
            return await _context.Statuses.FindAsync(id);
        }

        public async Task<IEnumerable<Status>> GetAllStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task UpdateStatusAsync(Status status)
        {
            _context.Statuses.Update(status);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStatusAsync(Guid id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();
            }
        }
    }

}
