using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface IInstructorWithdrawRepository : IRepository<InstructorWithdraw>
    {
        Task<IEnumerable<InstructorWithdraw>> GetWithdrawalsByUserIdAsync(string userId);
    }

}
