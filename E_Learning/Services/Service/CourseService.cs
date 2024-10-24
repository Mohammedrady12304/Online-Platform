using E_Learning.Areas.Course.Models;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repository.IReposatories;
using E_Learning.Services.IService;

namespace E_Learning.Services.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseViewModelRepository courseView;

        public CourseService(ICourseViewModelRepository courseView)
        {
            this.courseView = courseView;
        }
        public async Task<List<CourseView>> GetAllCourses()
        {
            return await courseView.GetCourseViewModels();
        }

        public async Task<List<CourseView>> GetCoursesWithSubCategory(string subCategoryId)
        {
            return await courseView.GetCourseViewModelsForsubcategory(subCategoryId);
        }

    }
}
