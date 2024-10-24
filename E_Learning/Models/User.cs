using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
    public class User: IdentityUser
    {
       
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The first name must contain only alphabetic characters.")]
        public string FName { get; set; }
        [MinLength(3,ErrorMessage ="First name must be at least 3 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The last name must contain only alphabetic characters.")]
        public  string LName { get; set; }
        //[DataType(DataType.EmailAddress)]
        //public  string Email { get; set; }
        public string? Image { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime LastLogin { get; set; }
        public Enrollment? Enrollment { get; set; }
        public DataForInstructor? DataForInstructor { get; set; }
		public List<UserAccount>? UserAccountes { get; set;}
        public List<Cart>? carts { get; set; }

    }
}
