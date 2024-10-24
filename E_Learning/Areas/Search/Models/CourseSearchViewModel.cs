using E_Learning.Models;

namespace E_Learning.Areas.Search.Models
{
    public class CourseSearchViewModel
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string summery { get; set; }
        public string InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
        public string Category { get; set; }
        public int? Rating { get; set; }
        public double Price { get; set; }
        public string Image;

        // New Properties
        public string Language { get; set; }  // e.g., "English", "Spanish"
        public string Level { get; set; }     // e.g., "Beginner", "Intermediate", "Advanced"
      

    }
}
