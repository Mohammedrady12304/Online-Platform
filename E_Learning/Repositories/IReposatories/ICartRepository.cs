using E_Learning.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.IReposatories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllAsync();
        Task<Cart> GetByIdAsync(string courseId, string userId);
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(string courseId, string userId);
        Task<IEnumerable<Cart>> GetCartsByUserIdAsync(string userId);
    }


}
