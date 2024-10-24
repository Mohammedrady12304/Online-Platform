using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class StudentCourseProgress
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("User")]
        public string StudentId { get; set; } = null!;
        public User? User { get; set; }
        [ForeignKey("Course")]
        public string CourseId { get; set; } = null!;
        public Course? Course { get; set; }
        public double ProgressRate { get; set; } = 0.0 ;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public string CourseTitle { get; set; } = null!;
        //public string InstructorName { get; set; } = null!;
        public string CourseThumbnail { get; set; } = null!;
    }

}
