using E_Learning.Areas.Authentication.Models;
using E_Learning.Models;
using E_Learning.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

namespace E_Learning.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender sender;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;
        public AuthService(UserManager<User> manager , IEmailSender sender , RoleManager<IdentityRole> roleManager , SignInManager<User> signInManager)
        {
            this.userManager = manager;
            this.sender = sender;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<ProcessResult> ChangePasswordAsync(ChangePasswordRequest model)
        {
            ProcessResult process = new ProcessResult();
            var user =await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    process.IsSucceded = true ;
                }
                string Error = string.Empty;
                foreach (var error in result.Errors)
                    Error += $"\n{error.Description} , ";
                process.Message = Error;
            }
            return new ProcessResult();
        }

        public async Task ConfirmEmailAsync(string Email)
        {
            var user =await userManager.FindByEmailAsync(Email);
            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);
            
        }

        public async Task<ProcessResult> LoginAsync(LoginRequest model)
        {
            ProcessResult process = new ProcessResult();
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                process.Message = "There Is no account for email: " + model.Email;
                return process;
            }
            var validPassword = await userManager.CheckPasswordAsync(user, model.Password);
            if (!validPassword)
            {
                process.Message = "Wrong Password";
                return process;
            }
            await signInManager.SignInAsync(user, isPersistent: model.RememberMe);
            process.IsSucceded = true;
            return process;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<ProcessResult> RegisterAsync(RegisterRequest model)
        {
            var user = new User
            {
                Email = model.Email,
                FName = model.FName,
                LName = model.LName,
                UserName = model.UserName,
                Image = "NotDefine.jpg",
                DateJoined = DateTime.Now,
                LastLogin = DateTime.Now,
                PhoneNumberConfirmed = false,
            };

            ProcessResult process = new ProcessResult();
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                process.IsSucceded = true;

                // Check if role exists, if not create the role
                if (!await roleManager.RoleExistsAsync(model.RegisteredAs))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = model.RegisteredAs,
                        NormalizedName = model.RegisteredAs.ToUpper()
                    });
                }

                // Add user to role
                await userManager.AddToRoleAsync(user, model.RegisteredAs);

                // Add a balance claim if the user is an instructor
                if (await userManager.IsInRoleAsync(user, "Instructor"))
                {
                    var claim = new Claim("Balance", "0");
                    await userManager.AddClaimAsync(user, claim);
                }
            }
            else
            {
                string Error = string.Empty;
                foreach (var error in result.Errors)
                {
                    Error += $"\n{error.Description}, ";
                }
                process.Message = Error;
            }

            return process;
        }


        public async Task<ProcessResult> ResetPasswordAsync([FromBody]ResetPasswordRequest model)
        {
            var user = await userManager.FindByEmailAsync(model.Email!);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, model.Password);
                if (result .Succeeded )
                {
                    return new ProcessResult { IsSucceded = true };
                }
                else
                {
                    string Error = string.Empty;
                    foreach (var error in result.Errors)
                        Error += $"\n{error.Description} , ";
                    return new ProcessResult { IsSucceded = false, Message = $"Error Reseting Password Due to {Error}" };
                }
            }
            return  new ProcessResult { };
        }

        public async Task<ProcessResult> SendConfirmationEmailAsync(ConfrimEmailRequest model)
        {
            var subject = "Confirmation Email";
            var body = $"<center><h1>Please Confirm Your Email Using Code Below</h1>\n" +
                   $"<h1 style=\"color:blue\">{model.Code}</h1></center>";
            var result = await GenerateEmailAsync(model.Email, subject, body);
            return result;
        }

        public async Task<ProcessResult> SendResetPassowrdEmail(ConfrimEmailRequest model)
        {
            var subject = "Rest Password Email";
            var body = $"<center><h1>Please Reset Your Password Using Code Below</h1>\n" +
                   $"<h1 style=\"color:blue\">{model.Code}</h1></center>";
            var result = await GenerateEmailAsync(model.Email, subject, body);
            return result;
        }

        private async Task<ProcessResult> GenerateEmailAsync(string Email , string subject , string body)
        {
            var user = await userManager.FindByEmailAsync(Email);
                try
            {
                await sender.SendEmailAsync(Email, subject, body);
                return new ProcessResult { IsSucceded = true};
            }
            catch (Exception ex)
            {
                return new ProcessResult { IsSucceded = false, Message = $"Error Sending Email  Due to {ex.Message}" };
            }
        }
    }
}
