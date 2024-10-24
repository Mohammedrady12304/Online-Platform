using E_Learning.Areas.Search.Models;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repository.IReposatories;

namespace E_Learning.Areas.Search.Data
{
    public class CourseSearchRepository : ICourseSearchRepository
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseSearchRepository(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<CourseSearchViewModel>> SearchCoursesAsync(
            string query,
            string category = null,
            int? minRating = null,
            double? minPrice = null,
            double? maxPrice = null,
            string language = null,
            string level = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            // Step 1: Fetch all courses or filtered by query
            var courses = await _courseRepository.GetCoursesAsync();

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                courses = courses.Where(c => c.Title.ToLower().Contains(query) || c.Description.ToLower().Contains(query));
            }

            // Step 2: Apply additional filters
            if (!string.IsNullOrEmpty(category))
            {
                courses = courses.Where(c => c.SubCategoryId == category);
            }
            if (minRating.HasValue)
            {
                courses = courses.Where(c => c.Rating >= minRating.Value);
            }
            if (minPrice.HasValue)
            {
                courses = courses.Where(c => c.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                courses = courses.Where(c => c.Price <= maxPrice.Value);
            }
            if (!string.IsNullOrEmpty(language))
            {
                courses = courses.Where(c => c.Language == language);
            }
            if (!string.IsNullOrEmpty(level))
            {
                courses = courses.Where(c => c.CourseLevel == level);
            }

            // Step 3: Fetch instructors and map the result to the ViewModel
            var viewModelResults = new List<CourseSearchViewModel>();
            foreach (var course in courses)
            {
                var instructor = await _userRepository.GetByIdAsync(course.InstructorId);
                var courseViewModel = new CourseSearchViewModel
                {
                    CourseId = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    summery = course.Summary,
                    Category = course.SubCategoryId,
                    Rating = course.Rating,
                    Price = course.Price,
                    Language = course.Language,
                    Level = course.CourseLevel,
                    InstructorName = instructor.FName + " " + instructor.LName,
                    InstructorEmail = instructor.Email,
                    InstructorId = instructor.Id,
                    Image = course.Image??"NotFound.png"
                };
                viewModelResults.Add(courseViewModel);
            }

            // Step 4: Paginate the results
            return PaginatedList<CourseSearchViewModel>.Create(viewModelResults, pageNumber, pageSize);
        }
    }
}
