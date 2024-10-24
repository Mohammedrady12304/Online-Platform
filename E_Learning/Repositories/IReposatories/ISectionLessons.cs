using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface ISectionLessonRepository : IRepository<SectionLessons>
    {
        Task<IEnumerable<SectionLessons>> GetLessonsBySectionIdAsync(string sectionId);
    }
}
