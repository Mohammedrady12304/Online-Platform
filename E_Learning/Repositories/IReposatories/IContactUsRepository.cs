using E_Learning.Repository.IReposatories;
using E_Learning.Models;

namespace E_Learning.Repositories.IReposatories
{
    public interface IContactUsRepository : IRepository<ContactUs>
    {
        IEnumerable<ContactUs> GetAll();
        void add(ContactUs contactUs);
    }
}
