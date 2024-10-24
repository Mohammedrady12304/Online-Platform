using E_Learning.Models;

namespace E_Learning.Repository.IReposatories
{
    public interface ICourseSectionRepository : IRepository<CourseSection>
    {
        Task<IEnumerable<CourseSection>> GetSectionsByCourseIdAsync(string courseId);
        Task<CourseSection> GetByIdLazyAsync(string id);
        Task<IEnumerable<CourseSection>> GetAllLazyAsync();
        Task<IEnumerable<CourseSection>> GetSectionsByCourseIdLazyAsync(string courseId);


	}

}
