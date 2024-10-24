using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class Payment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PaymentMethod { get; set; }
        public int PaymentAmount { get; set; }
        public DateTime PaymentTime { get; set; }
        [ForeignKey("Course")]
        public string CourseId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
    }
}
