using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Learning.Areas.Admin.Models
{
    public class CourseViewModel
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Summary { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? SelectedCourseLevel { get; set; }
        public IEnumerable<SelectListItem>? CourseLevels { get; set; } // Dropdown
        public string? SelectedLanguage { get; set; }
        public IEnumerable<SelectListItem>? Languages { get; set; } // Dropdown
        public string SubCategoryId { get; set; }
        public IEnumerable<SelectListItem>? SubCategories { get; set; } // Dropdown
        public string? InstructorId { get; set; }
        public int? Rating { get; set; }
        public string Status { get; set; }
        public IEnumerable<SelectListItem>? StatusList { get; set; }
        public IFormFile? Image { get; set; } // Image file for upload
        public bool AddSection { get; set; } = false;
    }


}
