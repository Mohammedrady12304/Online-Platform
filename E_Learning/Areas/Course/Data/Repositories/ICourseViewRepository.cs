using E_Learning.Areas.Course.Models;
using E_Learning.Repository.IReposatories;

namespace E_Learning.Areas.Course.Data.Repositories
{
	public interface ICourseViewRepository
	{
		Task<IEnumerable<CourseView>> GetAllAsync();

		Task<CourseView?> GetByIdAsync(string courseId);
	}

}
