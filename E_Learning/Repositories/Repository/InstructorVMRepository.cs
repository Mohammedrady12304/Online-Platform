using E_Learning.Repositories.IReposatories;
using E_Learning.ViewModels;

namespace E_Learning.Repositories.Repository
{
    public class InstructorVMRepository : IInstructorstatsVMRepository
    {
        public Task<InstructorStatisticsVM> GetById(string InstructorID)
        {
            throw new NotImplementedException();
        }
    }
}
