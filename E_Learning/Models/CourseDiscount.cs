using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class CourseDiscount
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Percentage { get; set; }
        public int NetPrice { get; set; }
        public DateTime ExpireDate { get; set; }
        [ForeignKey("Course")]
        public string CourseId { get; set; }
        public Course Course { get; set; }

    }
}
