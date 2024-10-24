using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class DataForInstructor
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Profession { get; set; }
        public string? Bio { get; set; }
        public int Balance { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; } = null!;
        public User? User { get; set; }
    }
}
