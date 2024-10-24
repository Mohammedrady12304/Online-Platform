using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(string userId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(string courseId);
    }

}
