using E_Learning.Models;
using E_Learning.Repository.IReposatories;

namespace E_Learning.Repositories.IReposatories
{
	public interface IUserRepository: IRepository<User>
	{
		Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string username);

    }

}
