using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId);
        Task<IEnumerable<Payment>> GetPaymentsByCourseIdAsync(string courseId);
    }

}
