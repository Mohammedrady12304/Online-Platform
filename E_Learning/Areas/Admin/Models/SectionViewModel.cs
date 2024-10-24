using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Areas.Admin.Models
{
    public class SectionViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public int order { get; set; }
        public string CourseId { get; set; }
        public bool AddLesson { get; set; } 

    }
}
