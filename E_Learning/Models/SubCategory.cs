using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class SubCategory
    {
        public string Id { get; set; }
        public string Title { get; set; }
       // [ForeignKey("Category")]
       // public string CategoryId { get; set; }
       // public Category Category { get; set; }
        public List<Course> Courses { get; set; }
       
    }
}
