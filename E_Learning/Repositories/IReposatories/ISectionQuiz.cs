using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface ISectionQuizRepository : IRepository<SectionQuiz>
    {
        Task<IEnumerable<SectionQuiz>> GetQuizzesBySectionIdAsync(string sectionId);
    }

}
