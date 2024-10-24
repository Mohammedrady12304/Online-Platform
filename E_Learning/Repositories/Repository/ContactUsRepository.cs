using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactUsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void add(ContactUs contactUs)
        {
            _context.Set<ContactUs>().Add(contactUs);
            _context.SaveChanges();
        }

        public async Task AddAsync(ContactUs entity)
        {
           await _context.Set<ContactUs>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(string id)
        {
            var contactUs = await _context.Set<ContactUs>().FindAsync(id);
            if(contactUs!=null)
            {
                _context.Set<ContactUs>().Remove(contactUs);
                _context.SaveChangesAsync();
            }
        }

        public IEnumerable<ContactUs> GetAll()
        {
            return _context.Set<ContactUs>().ToList();
        }

        public async Task<IEnumerable<ContactUs>> GetAllAsync()
        {
            return await _context.Set<ContactUs>().ToArrayAsync();
        }

        public async Task<ContactUs> GetByIdAsync(string id)
        {
            return await _context.Set<ContactUs>().FindAsync(id);
        }


        public async Task UpdateAsync(ContactUs New, ContactUs Old)
        {
            Old.FullName = New.FullName;
            Old.Email = New.Email;
            Old.Subject = New.Subject;
            Old.Message = New.Message;
            _context.Set<ContactUs>().Update(Old);
            await _context.SaveChangesAsync();
        }
    }
}
