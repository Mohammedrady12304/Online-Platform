using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesBySubCategoryAsync(string subCategoryId);
        Task<IEnumerable<Course>> GetCoursesByLevelAsync(string courseLevel);
        Task<IEnumerable<Course>> GetCoursesByPriceRangeAsync(double minPrice, double maxPrice);
        Task<IEnumerable<Course>> SearchCoursesAsync(string query);
        Task<IEnumerable<Course>> GetCoursesAsync();
        public Course GetById(string id);
    }
}
