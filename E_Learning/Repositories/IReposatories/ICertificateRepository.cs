using E_Learning.Models;

namespace E_Learning.Repository.IReposatories
{
    public interface ICertificateRepository : IRepository<Certificate>
    {
        Task<IEnumerable<Certificate>> GetCertificatesByUserIdAsync(string userId);
        Task<Certificate?> GetCertificateByCourseAndUserAsync(string courseId, string userId);
    }
}
