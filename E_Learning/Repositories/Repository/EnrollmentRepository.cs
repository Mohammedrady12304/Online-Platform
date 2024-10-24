using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Set<Enrollment>().ToListAsync();
        }

        public async Task<Enrollment> GetByIdAsync(string id)
        {
            return await _context.Set<Enrollment>().FindAsync(id);
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Set<Enrollment>().AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Enrollment enrollment , Enrollment enrollment1)
        {
            throw new Exception();
        }

        public async Task DeleteAsync(string id)
        {
            var enrollment = await _context.Set<Enrollment>().FindAsync(id);
            if (enrollment != null)
            {
                _context.Set<Enrollment>().Remove(enrollment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(string userId)
        {
            return await _context.Set<Enrollment>()
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(string courseId)
        {
            return await _context.Set<Enrollment>()
                .Where(e => e.CourseId == courseId)
                .ToListAsync();
        }
    }

}
