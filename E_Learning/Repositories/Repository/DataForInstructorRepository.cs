using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace E_Learning.Repositories.Repository
{

	public class DataForInstructorRepository : IDataForInstructorRepository
	{
		private readonly ApplicationDbContext _context;

		public DataForInstructorRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<DataForInstructor> GetInstructorDataByIdAsync(string id)
		{
			return await _context.DataForInstructors.FindAsync(id);
		}

		public async Task<IEnumerable<DataForInstructor>> GetAllInstructorsAsync()
		{
			return await _context.DataForInstructors.ToListAsync();
		}

		public async Task AddInstructorDataAsync(DataForInstructor dataForInstructor)
		{
			await _context.DataForInstructors.AddAsync(dataForInstructor);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateInstructorDataAsync(DataForInstructor dataForInstructor)
		{
			_context.DataForInstructors.Update(dataForInstructor);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteInstructorDataAsync(string id)
		{
			var instructorData = await GetInstructorDataByIdAsync(id);
			if (instructorData != null)
			{
				_context.DataForInstructors.Remove(instructorData);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<DataForInstructor> GetInstructorDataByInstructorIdAsync(string id)
		{
			return await _context.DataForInstructors.SingleOrDefaultAsync(x=>x.UserId == id);
		}
	}

}
