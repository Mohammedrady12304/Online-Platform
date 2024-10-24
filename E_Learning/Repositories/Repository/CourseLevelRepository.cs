using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class CourseLevelRepository : ICourseLevelRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseLevelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseLevel>> GetAllAsync()
        {
            return await _context.CourseLevels.ToListAsync();
        }

        public async Task<CourseLevel?> GetByIdAsync(string id)
        {
            return await _context.CourseLevels.FindAsync(id);
        }

        public async Task AddAsync(CourseLevel courseLevel)
        {
            _context.CourseLevels.Add(courseLevel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseLevel courseLevel)
        {
            _context.CourseLevels.Update(courseLevel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var courseLevel = await GetByIdAsync(id);
            if (courseLevel != null)
            {
                _context.CourseLevels.Remove(courseLevel);
                await _context.SaveChangesAsync();
            }
        }
    }

}
