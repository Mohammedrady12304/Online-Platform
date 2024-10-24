using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class CoursePreviewRepository : ICoursePreviewRepository
    {
        private readonly ApplicationDbContext _context;

        public CoursePreviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoursePreview>> GetAllAsync()
        {
            return await _context.Set<CoursePreview>().ToListAsync();
        }

        public async Task<CoursePreview> GetByIdAsync(string id)
        {
            return await _context.Set<CoursePreview>().FindAsync(id);
        }

        public async Task AddAsync(CoursePreview coursePreview)
        {
            await _context.Set<CoursePreview>().AddAsync(coursePreview);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CoursePreview old, CoursePreview New)
        {
            {
                old.Title = New.Title;
                old.Videourl = New.Videourl;
            }
            if (New != old) 
            { 
                New.Id = "vnfmcmkvcmkcx";
            }
            _context.Set<CoursePreview>().Update(old);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var coursePreview = await GetByIdAsync(id);
            if (coursePreview != null)
            {
                _context.Set<CoursePreview>().Remove(coursePreview);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CoursePreview> GetPreviewsByCourseIdAsync(string courseId)
        {
            return await _context.Set<CoursePreview>()
                .FirstOrDefaultAsync(cp => cp.CourseId == courseId);
        }
    }

}
