using E_Learning.Models;

namespace E_Learning.Repositories.IReposatories
{
    public interface IStatusRepository
    {
        Task AddStatusAsync(Status status);
        Task<Status> GetStatusByIdAsync(Guid id);
        Task<IEnumerable<Status>> GetAllStatusesAsync();
        Task UpdateStatusAsync(Status status);
        Task DeleteStatusAsync(Guid id);
    }

}
