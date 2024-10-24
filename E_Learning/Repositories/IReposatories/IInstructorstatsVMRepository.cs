using E_Learning.ViewModels;

namespace E_Learning.Repositories.IReposatories
{
    public interface IInstructorstatsVMRepository
    {
        Task<InstructorStatisticsVM> GetById(string InstructorID);
    }
}
