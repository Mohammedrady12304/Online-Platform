using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Cart> _dbSet;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Cart>();
        }

        // Get all Cart items
        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Course) // Eager load Course data if necessary
                .Include(c => c.User)   // Eager load User data if necessary
                .ToListAsync();
        }

        // Get Cart item by CourseId and UserId
        public async Task<Cart> GetByIdAsync(string courseId, string userId)
        {
            return await _dbSet
                .Include(c => c.Course)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CourseId == courseId && c.UserId == userId);
        }

        // Add a new Cart item
        public async Task AddAsync(Cart cart)
        {
            await _dbSet.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        // Update an existing Cart item
        public async Task UpdateAsync(Cart cart)
        {
            _dbSet.Update(cart);
            await _context.SaveChangesAsync();
        }

        // Delete a Cart item by CourseId and UserId
        public async Task DeleteAsync(string courseId, string userId)
        {
            var cart = await _dbSet.FirstOrDefaultAsync(c => c.CourseId == courseId && c.UserId == userId);
            if (cart != null)
            {
                _dbSet.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        // Get all Cart items for a specific User
        public async Task<IEnumerable<Cart>> GetCartsByUserIdAsync(string userId)
        {
            return await _dbSet
                .Where(c => c.UserId == userId)
                .Include(c => c.Course) // Eager load Course data
                .ToListAsync();
        }
    }
}
