using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;


        public CourseRepository(ApplicationDbContext  context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Set<Course>().Include(c=>c.carts).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(string id)
        {
            return await _context.Set<Course>().FindAsync(id);
        }

        public async Task AddAsync(Course course)
        {
            await _context.Set<Course>().AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course old , Course New)
        {
            {
                old.CourseLevel = New.CourseLevel;
                old.Title = New.Title;
                old.Duration = New.Duration;
                old.Title = New.Title;
                old.Description = New.Description;
                old.CourseLevel = New.CourseLevel;
                old.Status  = New.Status;
                old.Image = New.Image;
                old.Summary = New.Summary;
                old.Language = New.Language;
                old.SubCategoryId = New.SubCategoryId;
                old.InstructorId = New.InstructorId;
                old.Price = New.Price;
            }
            if (New != old)
            {
                New.Id = Guid.NewGuid().ToString();
            }
            _context.Set<Course>().Update(old);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var course = await _context.Set<Course>().FindAsync(id);
            if (course != null)
            {
                _context.Set<Course>().Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesBySubCategoryAsync(string subCategoryId)
        {
            return await _context.Set<Course>()
                .Where(c => c.SubCategoryId == subCategoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByLevelAsync(string courseLevel)
        {
            return await _context.Set<Course>()
                .Where(c => c.CourseLevel == courseLevel)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByPriceRangeAsync(double minPrice, double maxPrice)
        {
            return await _context.Set<Course>()
                .Where(c => c.Price  >= minPrice && c.Price <= maxPrice)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.Include(c => c.user).ToListAsync();
        }


        public async Task<IEnumerable<Course>> SearchCoursesAsync(string query)
        {
            query = query.ToLower();

            return await _context.Courses
                .Include(c => c.user) // Include the Instructor (User)
                .Where(c => c.Title.ToLower().Contains(query) || c.Description.ToLower().Contains(query))
                .ToListAsync();
        }

        // Helper method to preprocess the query
        private string PreprocessQuery(string query, string[] stopWords)
        {
            return string.Join(" ", query.Split(' ').Where(word => !stopWords.Contains(word.ToLower())));
        }

        public Course GetById(string id)
        {
            return _context.Set<Course>().Include(c => c.user).ThenInclude(u => u.DataForInstructor).Include(c => c.courseSections).ThenInclude(s => s.SectionLessons).FirstOrDefault(c => c.Id == id);
        }

    }

}
