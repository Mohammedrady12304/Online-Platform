using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly ApplicationDbContext _context;

        public WishListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishList>> GetAllAsync()
        {
            return await _context.Set<WishList>()
                .Include(w => w.Course) // Include related course
                .Include(w => w.User)   // Include related user
                .ToListAsync();
        }

        public async Task<WishList> GetByIdAsync(string id)
        {
           throw new NotImplementedException();
        }

        public async Task AddAsync(WishList wishList)
        {
            await _context.Set<WishList>().AddAsync(wishList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WishList wishList1 , WishList wishList2)
        {
            _context.Set<WishList>().Update(wishList1);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var wishList = await _context.Set<WishList>().FindAsync(id);
            if (wishList != null)
            {
                _context.Set<WishList>().Remove(wishList);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<WishList>> GetWishListsByUserIdAsync(string userId)
        {
            return await _context.Set<WishList>()
                .Include(w => w.Course) // Include related course
                .Include(w => w.User)   // Include related user
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<WishList> GetWishListByUserAndCourseIdAsync(string userId, string courseId)
        {
            return await _context.Set<WishList>()
                .FirstOrDefaultAsync(w => w.UserId == userId && w.CourseId == courseId);
        }

        public async Task<bool> DeleteAsync(string userId, string CourseId)
        {
            var wishList = await _context.Set<WishList>().FirstOrDefaultAsync(x=>x.UserId == userId && x.CourseId == CourseId);
            if (wishList != null)
            {
                _context.Set<WishList>().Remove(wishList);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }

}
