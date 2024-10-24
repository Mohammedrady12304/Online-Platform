namespace E_Learning.Areas.Student.Data.Services
{
    public interface ILearningService
    {
        Task UpdateCourseProgressAsync(string studentId, string courseId, double newProgress);
    }      
}
