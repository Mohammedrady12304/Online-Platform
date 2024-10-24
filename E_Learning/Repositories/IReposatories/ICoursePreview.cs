using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface ICoursePreviewRepository : IRepository<CoursePreview>
    {
        Task<CoursePreview> GetPreviewsByCourseIdAsync(string courseId);
    }

}
