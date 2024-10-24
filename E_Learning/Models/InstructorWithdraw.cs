using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class InstructorWithdraw
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Amount { get; set; }
        public string Method { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
