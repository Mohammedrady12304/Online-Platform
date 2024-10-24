using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class Enrollment
    {
        public  string  Id { get; set; } = Guid.NewGuid().ToString();
        public bool CompletionStatus { get; set; } = false;
        public DateTime Date { get; set; }
        public int Percentage { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Course")]
        public string CourseId { get; set; }
        public User User { get; set; }
        public Course Course { get; set; }
    }
}
