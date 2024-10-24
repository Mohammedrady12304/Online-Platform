using E_Learning.Areas.Course.Models;
using E_Learning.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Areas.Course.Data.Repositories
{
	public class CourseViewRepository : ICourseViewRepository
	{
		private readonly ApplicationDbContext _context;

		public CourseViewRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		// Get all courses
		public async Task<IEnumerable<CourseView>> GetAllAsync()
		{
			return await _context.Set<CourseView>().ToListAsync();
		}

		// Get a course by its ID
		public async Task<CourseView?> GetByIdAsync(string courseId)
		{
			return await _context.CourseViewModels.FindAsync(courseId);
		}


	}

}
