using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
    public class CourseLevel
    {
        [Key]
        public string NameId { get; set; } = null!;

        //public List<Course>? course {  get; set; }


        //public CourseLevel()
        //{
        //    NameId = Guid.NewGuid().ToString();
        //}
    }
}
