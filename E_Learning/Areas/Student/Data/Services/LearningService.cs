using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repository.IReposatories;

namespace E_Learning.Areas.Student.Data.Services
{
    public class LearningService : ILearningService
    {
        private readonly IStudentCourseProgressRepository _progressRepository;
        private readonly ICourseRepository _courseRepository;
        public LearningService(IStudentCourseProgressRepository progressRepository , ICourseRepository course )
        {
            _progressRepository = progressRepository;
            _courseRepository = course;
           
        }

        public async Task UpdateCourseProgressAsync(string studentId, string courseId, double newProgress)
        {
            var progress = _progressRepository.GetProgress(studentId, courseId);
            if (progress != null)
            {
                progress.ProgressRate = newProgress;
                progress.LastUpdated = DateTime.Now;
                _progressRepository.UpdateProgress(progress);
            }
            else
            {
                var Course = await _courseRepository.GetByIdAsync(courseId);
                _progressRepository.AddProgress(new StudentCourseProgress
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    CourseTitle = Course.Title ,
                    CourseThumbnail = Course.Image ,
                    ProgressRate = newProgress,
                    LastUpdated = DateTime.Now
                });
            }
        }

    }
}
