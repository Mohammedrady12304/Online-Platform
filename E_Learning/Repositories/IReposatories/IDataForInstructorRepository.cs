using E_Learning.Models;

namespace E_Learning.Repositories.IReposatories
{
	public interface IDataForInstructorRepository
	{
		Task<DataForInstructor> GetInstructorDataByIdAsync(string id);
		Task<DataForInstructor> GetInstructorDataByInstructorIdAsync(string id);
		Task<IEnumerable<DataForInstructor>> GetAllInstructorsAsync();
		Task AddInstructorDataAsync(DataForInstructor dataForInstructor);
		Task UpdateInstructorDataAsync(DataForInstructor dataForInstructor);
		Task DeleteInstructorDataAsync(string id);
	}
}
