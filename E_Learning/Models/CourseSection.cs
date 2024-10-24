using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class CourseSection
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public int order { get; set; }
        [ForeignKey("Course")]
        public string CourseId { get; set; }
        public Course Course { get; set; }

        public List<SectionLessons>? SectionLessons { get; set; }
    }
}
