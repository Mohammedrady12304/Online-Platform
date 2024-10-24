using E_Learning.ViewModels;

namespace E_Learning.Services.IService
{
    public interface IInstructorService
    {
        Task<InstructorStatisticsVM> GetStatistics(string InstructorID);
    }
}
