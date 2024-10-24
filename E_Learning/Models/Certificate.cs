using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class Certificate
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public DateTime IssueTime { get; set; } 
        [ForeignKey("Course")]
        public string? CourseId  { get; set; }
		public Course? Course { get; set; }

		[ForeignKey("User")]
        public string UserId { get; set; }  = null!;
        public  User? User { get; set; }
    }
}
