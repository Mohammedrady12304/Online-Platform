using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class AboutUsRepository : IAboutUsRepository
    {

        private readonly ApplicationDbContext _context;
        public AboutUsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AboutUs aboutUs)
        {
            await _context.Set<AboutUs>().AddAsync(aboutUs);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var aboutUs = await _context.Set<AboutUs>().FindAsync(id);
            if (aboutUs != null)
            {
                _context.Set<AboutUs>().Remove(aboutUs);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<AboutUs> GetAll()
        {
            return _context.Set<AboutUs>().ToList();
        }

        public async Task<IEnumerable<AboutUs>> GetAllAsync()
        {
            return await _context.Set<AboutUs>().ToListAsync();
        }

        public async Task<AboutUs> GetByIdAsync(string id)
        {
            return await _context.Set<AboutUs>().FindAsync(id);
        }

        public async Task UpdateAsync(AboutUs New, AboutUs Old)
        {
        
        Old.Title = New.Title;
            Old.Content = New.Content;
            Old.ImageUrl = New.ImageUrl;
            Old.Content = New.Content;
            Old.Mission = New.Mission;
            Old.ContactEmail = New.ContactEmail;
            Old.ContactPhone = New.ContactPhone;
            Old.FacebookLink = New.FacebookLink;
            Old.LinkedInLink = New.LinkedInLink;
            Old.TwitterLink = New.TwitterLink;
            Old.UpdatedAt = DateTime.Now;
            _context.Set<AboutUs>().Update(Old);
            await _context.SaveChangesAsync();

        }
    }
}
