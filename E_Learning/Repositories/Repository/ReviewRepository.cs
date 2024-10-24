using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Set<Review>().ToListAsync();
        }

        public async Task<Review> GetByIdAsync(string id)
        {
            return await _context.Set<Review>().FindAsync(id);
        }

        public async Task AddAsync(Review review)
        {
            await _context.Set<Review>().AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review1, Review review2)
        {
            {
                review1.Evaluation = review2.Evaluation;
                review1.Date = DateTime.Now;
                review1.FeedBack = review2.FeedBack;
            }
            _context.Set<Review>().Update(review1);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var review = await _context.Set<Review>().FindAsync(id);
            if (review != null)
            {
                _context.Set<Review>().Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Review>> GetReviewsByCourseIdAsync(string courseId)
        {
            return await _context.Set<Review>()
                .Where(r => r.CorurseId == courseId) // Note: there's a typo in the property name
                .ToListAsync();
        }
    }

}
