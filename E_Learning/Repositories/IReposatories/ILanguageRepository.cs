using E_Learning.Models;

namespace E_Learning.Repositories.IReposatories
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllAsync();
        Task<Language?> GetByIdAsync(string id);
        Task AddAsync(Language language);
        Task UpdateAsync(Language language);
        Task DeleteAsync(string id);
    }
}
