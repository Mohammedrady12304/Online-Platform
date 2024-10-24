using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByCourseIdAsync(string courseId);
    }

}
