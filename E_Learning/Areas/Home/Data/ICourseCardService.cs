using E_Learning.Areas.Home.Models;

namespace E_Learning.Areas.Home.Data
{
    public interface ICourseCardService
    {
        public Task<IEnumerable<CourseCardDetails>> getAll();
        public Task<CourseCardDetails> getCourseAsync(string id);
    }
}
