using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Attributes
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userRepository = (IUserRepository)validationContext.GetService(typeof(IUserRepository));

            if (userRepository == null)
            {
                throw new InvalidOperationException("Unable to resolve IUserRepository.");
            }

            var userName = value as string;

           
            var userExists = userRepository.GetByEmailAsync(userName).Result != null;

            if (userExists)
            {
                return new ValidationResult($"The Email '{userName}' is already registered.");
            }

            return ValidationResult.Success!;
        }
    }

}
