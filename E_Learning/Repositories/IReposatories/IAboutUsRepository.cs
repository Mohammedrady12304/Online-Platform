using E_Learning.Models;
using E_Learning.Repository.IReposatories;

namespace E_Learning.Repositories.IReposatories
{
    public interface IAboutUsRepository : IRepository<AboutUs>
    {

        IEnumerable<AboutUs> GetAll();

    }
}
