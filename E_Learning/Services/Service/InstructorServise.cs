using E_Learning.Repositories.IReposatories;
using E_Learning.Services.IService;
using E_Learning.ViewModels;

namespace E_Learning.Services.Service
{
    public class InstructorServise : IInstructorService
    {
        private readonly IInstructorstatsVMRepository repo;

        public InstructorServise(IInstructorstatsVMRepository Repo)
        {
            repo = Repo;
        }
        public async Task<InstructorStatisticsVM> GetStatistics(string InstructorID)
        {
            return await repo.GetById(InstructorID);
        }
    }
}
