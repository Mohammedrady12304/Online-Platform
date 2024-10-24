using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Attributes
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userRepository = (IUserRepository)validationContext.GetService(typeof(IUserRepository));

            if (userRepository == null)
            {
                throw new InvalidOperationException("Unable to resolve IUserRepository.");
            }

            var userName = value as string;

            // Perform the async check (simplified here for synchronous context)
            var userExists = userRepository.GetByUserNameAsync(userName).Result !=null;

            if (userExists)
            {
                return new ValidationResult($"The username '{userName}' is already taken.");
            }

            return ValidationResult.Success!;
        }
    }

}
