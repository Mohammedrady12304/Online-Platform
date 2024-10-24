using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface IWishListRepository : IRepository<WishList>
    {
        Task<IEnumerable<WishList>> GetWishListsByUserIdAsync(string userId);
        Task<bool> DeleteAsync(string userId , string CourseId);

    }

}
