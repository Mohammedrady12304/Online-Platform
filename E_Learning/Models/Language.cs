using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
    public class Language
    {
        [Key]
        public string NameId { get; set; } = null!;

        //public List<Course>? course { get; set; }


        //public Language()
        //{
        //    NameId = Guid.NewGuid().ToString();
        //}
    }
}
