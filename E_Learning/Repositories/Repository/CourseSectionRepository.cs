using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace E_Learning.Repositories.Repository
{
    public class CourseSectionRepository : ICourseSectionRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseSectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseSection>> GetAllAsync()
        {
            return await _context.Set<CourseSection>().ToListAsync();
        }

        public async Task<CourseSection> GetByIdAsync(string id)
        {
            return await _context.Set<CourseSection>().FindAsync(id);
        }

        public async Task AddAsync(CourseSection section )
        {
            await _context.Set<CourseSection>().AddAsync(section);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseSection Old, CourseSection New)
        {
            {
                Old.Title = New.Title;
                Old.order = New.order;
            }
            _context.Set<CourseSection>().Update(Old);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var courseSection = await _context.Set<CourseSection>().FindAsync(id);
            if (courseSection != null)
            {
                //may Edit Order
                _context.Set<CourseSection>().Remove(courseSection);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CourseSection>> GetSectionsByCourseIdAsync(string courseId)
        {
            return await _context.Set<CourseSection>()
                .Where(cs => cs.CourseId == courseId)
                .OrderBy(cs => cs.order)
                .ToListAsync();
        }

		public async Task<IEnumerable<CourseSection>> GetSectionsByCourseIdLazyAsync(string courseId)
		{
			return await _context.Set<CourseSection>().Include(cs => cs.SectionLessons)
				.Where(cs => cs.CourseId == courseId)
				.OrderBy(cs => cs.order)
				.ToListAsync();
		}

		public async Task<CourseSection> GetByIdLazyAsync(string id)
		{
			return await _context.Set<CourseSection>().Include(s => s.SectionLessons).FirstAsync(x=>x.Id == id);
		}

		public async Task<IEnumerable<CourseSection>> GetAllLazyAsync()
		{
			return await _context.Set<CourseSection>().Include(s => s.SectionLessons).ToListAsync();
		}
	}

}
