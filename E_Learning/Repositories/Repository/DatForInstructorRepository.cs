using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace E_Learning.Repositories.Repository
{
    public class DatForInstructorRepository : IDataForInstructor
    {
        private readonly ApplicationDbContext context;

        public DatForInstructorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(DataForInstructor entity)
        {
            await context.DataForInstructors.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var instructorData = await GetByIdAsync(id);
            if (instructorData != null)
            {
                context.DataForInstructors.Remove(instructorData);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DataForInstructor>> GetAllAsync()
        {
            return await context.DataForInstructors.ToListAsync();
        }

        public async Task<DataForInstructor> GetByIdAsync(string id)
        {
            return await context.DataForInstructors.FindAsync(id);
        }

        public async Task UpdateAsync(DataForInstructor Old, DataForInstructor New)
        {
            {
                Old.Balance = New.Balance;
                Old.Profession = New.Profession;
                Old.Bio = New.Bio;
            }
            if(New != Old)
            {
                New.Id = "cnkmdjfsnkd";
            }
            if (Old != null)
            {
                context.DataForInstructors.Update(Old);
                await context.SaveChangesAsync();
            }
        }
    }
}
