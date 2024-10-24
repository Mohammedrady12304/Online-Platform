namespace E_Learning.Repositories.IReposatories
{
	using E_Learning.Models;
	using E_Learning.Repository.IReposatories;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IUserAccountRepository : IRepository<UserAccount>
	{
		Task<IEnumerable<UserAccount>> GetUserAccountsByUserIdAsync(string userId);
		Task<IEnumerable<UserAccount>> GetUserAccountsBySocialMediaIdAsync(string socialMediaId);
		Task<UserAccount?> GetUserAccountAsync(string userId, string socialMediaId);
	}

}
