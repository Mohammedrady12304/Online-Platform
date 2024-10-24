using E_Learning.Areas.Search.Models;

namespace E_Learning.Areas.Search.Data
{
    public interface ICourseSearchRepository
    {
      Task<IEnumerable<CourseSearchViewModel>> SearchCoursesAsync(
      string query,
      string category = null,
      int? minRating = null,
      double? minPrice = null,
      double? maxPrice = null,
      string language = null,
      string level = null,
      int pageNumber = 1,
      int pageSize = 10);
    }
}
