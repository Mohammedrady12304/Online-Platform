using E_Learning.Areas.Course.Models;

namespace E_Learning.Repositories.IReposatories
{
    public interface ICourseViewModelRepository
    {
        Task<List<CourseView>> GetCourseViewModelsForsubcategory(string subCategory);
        Task<List<CourseView>> GetCourseViewModels();
    }
}
