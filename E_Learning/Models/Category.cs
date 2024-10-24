using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        //public List<SubCategory>? SubCategories {  get; set; }
        
       // public List<Course>? Courses {  get; set; }

        //  [ForeignKey(nameof(SuperCategory))]
        // public string? SuperCategotryId { get; set; }
        //  public SuperCategory? SuperCategory { get; set; }

        public Category() 
        { 
            Id = Guid.NewGuid().ToString();
        }
    }
}
