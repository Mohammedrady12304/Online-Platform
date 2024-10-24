using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class Cart
    {
        [ForeignKey(nameof(Course))]
        public string CourseId { get; set; }
        public Course? Course { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set;}
        public User? User { get; set; }

        //public int order { get; set;}

    }
}
