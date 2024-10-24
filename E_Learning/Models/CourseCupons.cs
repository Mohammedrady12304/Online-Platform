using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class CourseCupons
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Code { get; set; } // may make index for it
        public int UsageLimit { get; set; }
        public int NumberOfUsages { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime ExpireDate { get; set; }
        public int AvailDuration { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("Course")]
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
