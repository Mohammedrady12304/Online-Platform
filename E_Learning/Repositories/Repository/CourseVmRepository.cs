using E_Learning.Areas.Course.Models;
using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class CourseVmRepository : ICourseViewModelRepository
    {
        private readonly ApplicationDbContext context;
        
        public CourseVmRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<CourseView>> GetCourseViewModels()
        {
            return await context.CourseViewModels.ToListAsync();
        }

        public async Task<List<CourseView>> GetCourseViewModelsForsubcategory(string subCategoryId)
        {
            return await context.CourseViewModels.Where(C => C.SubCategoryId == subCategoryId).ToListAsync();
        }
    }
}
