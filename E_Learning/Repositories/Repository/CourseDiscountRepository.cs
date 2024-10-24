using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class CourseDiscountRepository : ICourseDiscountRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseDiscountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseDiscount>> GetAllAsync()
        {
            return await _context.Set<CourseDiscount>().ToListAsync();
        }

        public async Task<CourseDiscount> GetByIdAsync(string id)
        {
            var discount = await _context.Set<CourseDiscount>().FindAsync(id);
            return discount;
        }

        public async Task AddAsync(CourseDiscount courseDiscount)
        {
            await _context.Set<CourseDiscount>().AddAsync(courseDiscount);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseDiscount OldDiscount , CourseDiscount NewDiscount)
        {
            {
                OldDiscount.ExpireDate = NewDiscount.ExpireDate;
                OldDiscount.Percentage = NewDiscount.Percentage;
                OldDiscount.NetPrice = OldDiscount.NetPrice * (100 - NewDiscount.Percentage);
            }
            _context.Set<CourseDiscount>().Update(OldDiscount);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var courseDiscount = await _context.Set<CourseDiscount>().FindAsync(id);
            if (courseDiscount != null)
            {
                _context.Set<CourseDiscount>().Remove(courseDiscount);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CourseDiscount> GetDiscountsByCourseIdAsync(string courseId)
        {
            return await _context.Set<CourseDiscount>()
                .FirstOrDefaultAsync(cd => cd.CourseId == courseId && cd.ExpireDate <= DateTime.Now);
        }

        public async Task<IEnumerable<CourseDiscount>> GetActiveDiscountsAsync()
        {
            return await _context.Set<CourseDiscount>()
                .Where(cd => cd.ExpireDate > DateTime.Now)
                .ToListAsync();
        }
    }

}
