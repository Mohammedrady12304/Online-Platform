using E_Learning.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Areas.Authentication.Models
{
    public class RegisterRequest
    {
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The first name must contain only alphabetic characters.")]
        [Required]
        public string FName { get; set; } = null!;

        [MinLength(3, ErrorMessage = "First name must be at least 3 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The last name must contain only alphabetic characters.")]
        [Required]
        public string LName { get; set; }

        [MinLength(3, ErrorMessage = "First name must be at least 3 characters")]
        [RegularExpression("^[a-zA-Z_]+[a-zA-Z_0-9]*$", ErrorMessage = "UserName is not Valid")]
        [Required]
        [UniqueUsername]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [UniqueEmail]
        public string Email { get; set; } = null!;

       
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }

        public string RegisteredAs { get; set; }

    }
}
