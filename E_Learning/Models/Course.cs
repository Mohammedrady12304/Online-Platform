using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class Course
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public  string Description { get; set; }
        
        public int Duration { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CourseLevel { get; set; }
        public int NumberOfRegisters { get; set; } = 0;
        public int? Rating { get; set; }
        public string Image { get; set; } 
        [ForeignKey("SubCategory")]
        public string SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public List<CoursePreview>? Previews { get; set; }
        public List<CourseSection>? courseSections { get; set; }
        public List<WishList>? WishLists { get; set; }

        [ForeignKey("user")]
        public string InstructorId { get; set; }
        public User user { get; set; }


        public string? Language { get; set; }
        
        public List<Cart>? carts { get; set; }

    }
}
