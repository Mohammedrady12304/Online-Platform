using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
    public class Status
    {
        [Key]
        public string NameId { get; set; } = Guid.NewGuid().ToString();  
        public string StatusDescription { get; set; } 
    }

}
