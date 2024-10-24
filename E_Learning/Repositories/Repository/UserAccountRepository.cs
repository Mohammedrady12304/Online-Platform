namespace E_Learning.Repositories.Repository
{
	using E_Learning.Models;
	using E_Learning.Repositories.IReposatories;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class UserAccountRepository : IUserAccountRepository
	{
		private readonly ApplicationDbContext _context;

		public UserAccountRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		// Generic CRUD Operations
		public async Task<IEnumerable<UserAccount>> GetAllAsync()
		{
			return await _context.userAccounts
				.Include(ua => ua.User)
				.Include(ua => ua.SocialMedia)
				.ToListAsync();
		}

		public async Task<UserAccount?> GetByIdAsync(string id)
		{
			return await _context.userAccounts
				.Include(ua => ua.User)
				.Include(ua => ua.SocialMedia)
				.FirstOrDefaultAsync(ua => ua.Id == id);
		}

		public async Task AddAsync(UserAccount userAccount)
		{
			await _context.userAccounts.AddAsync(userAccount);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(UserAccount userAccount)
		{
			_context.userAccounts.Update(userAccount);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string id)
		{
			var userAccount = await _context.userAccounts.FindAsync(id);
			if (userAccount != null)
			{
				_context.userAccounts.Remove(userAccount);
				await _context.SaveChangesAsync();
			}
		}

		// Specific Methods
		public async Task<IEnumerable<UserAccount>> GetUserAccountsByUserIdAsync(string userId)
		{
			return await _context.userAccounts
				.Include(ua => ua.User)
				.Include(ua => ua.SocialMedia)
				.Where(ua => ua.UserID == userId)
				.ToListAsync();
		}

		public async Task<IEnumerable<UserAccount>> GetUserAccountsBySocialMediaIdAsync(string socialMediaId)
		{
			return await _context.userAccounts
				.Include(ua => ua.User)
				.Include(ua => ua.SocialMedia)
				.Where(ua => ua.SocialMediaID == socialMediaId)
				.ToListAsync();
		}

		public async Task<UserAccount?> GetUserAccountAsync(string userId, string socialMediaId)
		{
			return await _context.userAccounts
				.Include(ua => ua.User)
				.Include(ua => ua.SocialMedia)
				.FirstOrDefaultAsync(ua => ua.UserID == userId && ua.SocialMediaID == socialMediaId);
		}

		public async Task UpdateAsync(UserAccount New, UserAccount Old)
		{
			Old.url = New.url;
			_context.Update(Old);
			await _context.SaveChangesAsync();
		}
	}

}
