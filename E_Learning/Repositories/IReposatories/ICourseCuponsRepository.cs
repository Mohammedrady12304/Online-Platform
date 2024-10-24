using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface ICourseCuponsRepository : IRepository<CourseCupons>
    {
        Task<CourseCupons> GetByCodeAsync(string code);
        Task<IEnumerable<CourseCupons>> GetActiveCouponsAsync();
    }
}
