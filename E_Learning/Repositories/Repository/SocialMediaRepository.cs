using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
	public class SocialMediaRepository : ISocialMediaRepository
	{
		private readonly DbContext _context;

		public SocialMediaRepository(DbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<SocialMedia>> GetAllAsync()
		{
			return await _context.Set<SocialMedia>()
				.Include(sm => sm.UserAccountes) // Include related user accounts
				.ToListAsync();
		}

		public async Task<SocialMedia?> GetByIdAsync(string id)
		{
			return await _context.Set<SocialMedia>()
				.Include(sm => sm.UserAccountes)
				.FirstOrDefaultAsync(sm => sm.Id == id);
		}

		public async Task AddAsync(SocialMedia socialMedia)
		{
			await _context.Set<SocialMedia>().AddAsync(socialMedia);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(SocialMedia socialMedia)
		{
			_context.Set<SocialMedia>().Update(socialMedia);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string id)
		{
			var socialMedia = await _context.Set<SocialMedia>().FindAsync(id);
			if (socialMedia != null)
			{
				_context.Set<SocialMedia>().Remove(socialMedia);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<SocialMedia?> GetByNameAsync(string name)
		{
			return await _context.Set<SocialMedia>()
				.Include(sm => sm.UserAccountes)
				.FirstOrDefaultAsync(sm => sm.Name == name);
		}

		public async Task UpdateAsync(SocialMedia New, SocialMedia Old)
		{
			Old.Name = New.Name;
			Old.Icon = New.Icon;
			_context.Update(Old);
			await _context.SaveChangesAsync();
		}

	}

}
