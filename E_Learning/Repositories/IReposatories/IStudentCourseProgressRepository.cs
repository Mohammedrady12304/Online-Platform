using E_Learning.Models;

namespace E_Learning.Repositories.IReposatories
{
    public interface IStudentCourseProgressRepository
    {
       
        IEnumerable<StudentCourseProgress> GetProgressByStudent(string studentId);

        StudentCourseProgress GetProgress(string studentId, string courseId);

        void AddProgress(StudentCourseProgress progress);

        void UpdateProgress(StudentCourseProgress progress);

        void DeleteProgress(string progressId);

        void SaveChanges();
    }

}
