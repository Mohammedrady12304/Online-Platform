using E_Learning.Models;

namespace E_Learning.Repository.IReposatories
{
    public interface ICourseDiscountRepository : IRepository<CourseDiscount>
    {
        Task<CourseDiscount> GetDiscountsByCourseIdAsync(string courseId);
        Task<IEnumerable<CourseDiscount>> GetActiveDiscountsAsync();
    }

}
