using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Areas.Admin.Models
{
    public class LessonViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public int Order { get; set; }
        public string Videourl { get; set; }
        public string? AttachedFile { get; set; }
        [ForeignKey("CourseSection")]
        public string SectionId { get; set; }
        public IEnumerable<SelectListItem>? sections { get; set; }

    }
}
