using E_Learning.Models;
using E_Learning.Repositories.IReposatories;

namespace E_Learning.Repositories.Repository
{
    public class StudentCourseProgressRepository : IStudentCourseProgressRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentCourseProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StudentCourseProgress> GetProgressByStudent(string studentId)
        {
            return _context.Progresses
                           .Where(p => p.StudentId == studentId)
                           .ToList();
        }

        public StudentCourseProgress GetProgress(string studentId, string courseId)
        {
            return _context.Progresses
                           .FirstOrDefault(p => p.StudentId == studentId && p.CourseId == courseId);
        }

        public void AddProgress(StudentCourseProgress progress)
        {
            _context.Progresses.Add(progress);
            SaveChanges();
        }

        public void UpdateProgress(StudentCourseProgress progress)
        {
            _context.Progresses.Update(progress);
            SaveChanges();
        }

        public void DeleteProgress(string progressId)
        {
            var progress = _context.Progresses.Find(progressId);
            if (progress != null)
            {
                _context.Progresses.Remove(progress);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}
