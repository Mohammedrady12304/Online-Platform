using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Set<Payment>().ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(string id)
        {
            return await _context.Set<Payment>().FindAsync(id);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Set<Payment>().AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment , Payment New)
        {
            throw new Exception();
        }

        public async Task DeleteAsync(string id)
        {
            var payment = await _context.Set<Payment>().FindAsync(id);
            if (payment != null)
            {
                _context.Set<Payment>().Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId)
        {
            return await _context.Set<Payment>()
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByCourseIdAsync(string courseId)
        {
            return await _context.Set<Payment>()
                .Where(p => p.CourseId == courseId)
                .ToListAsync();
        }
    }

}
