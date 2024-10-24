using E_Learning.Areas.Course.Models;

namespace E_Learning.Areas.Course.Data.Services
{
	public interface ICourseFullDataViewModelService
	{
		Task<CourseFullDataViewModel> GetFullDataByIdAsync(string Id);

	}
}
