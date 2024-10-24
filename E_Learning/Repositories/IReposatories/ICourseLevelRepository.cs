using E_Learning.Models;

namespace E_Learning.Repositories.IReposatories
{
    public interface ICourseLevelRepository
    {
        Task<IEnumerable<CourseLevel>> GetAllAsync();
        Task<CourseLevel?> GetByIdAsync(string id);
        Task AddAsync(CourseLevel courseLevel);
        Task UpdateAsync(CourseLevel courseLevel);
        Task DeleteAsync(string id);
    }

}
