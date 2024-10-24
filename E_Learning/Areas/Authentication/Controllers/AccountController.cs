using E_Learning.Areas.Authentication.Models;
using E_Learning.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Learning.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AccountController : Controller
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        #region Register
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View("Register" , new RegisterRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRegister(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.RegisterAsync(model);
                if (!result.IsSucceded)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View("Register", model);  
                }
                else
                {
                    var loginData = new LoginRequest { Email = model.Email, Password = model.Password, RememberMe = true };
                    return  RedirectToAction("ShowLogin");
                }
            }
            return View("Register", model);  
        }



        #endregion

        #region Confirm Email
        public async Task<IActionResult> SendConfirmEmail(string email)
        {
            TempData["ConfirmEmailCode"] = await GenerateCode();
            var model = new ConfrimEmailRequest
            {
                Email = email,
                Code = Convert.ToInt32(TempData.Peek("ConfirmEmailCode"))
            };
            var result = await authService.SendConfirmationEmailAsync(model);
            return RedirectToAction(" ConfirmEmail", model);
        }
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfrimEmailRequest model)
        {
            //may make a counter
            var correctCode = Convert.ToInt32(TempData.Peek("ConfirmEmailCode"));
            if (model.Code == correctCode)
            {
                await authService.ConfirmEmailAsync(model.Email);
                TempData.Remove("ConfirmEmailCode");
            }
            else
            {
                ViewBag.Message = "Error code, Please try again!";
            }
            return Ok();
        }
        public async Task<IActionResult> ResendConfirmEmail()
        {
            if (TempData.ContainsKey("ConfirmEmailCode"))
            {
                return Content("you can request another code after 5 minutes");
            }
            return Ok();
        }
        #endregion

        #region Password Reset & change
        public async Task<IActionResult> ForgetPassword()
        {
            return Ok();
        }
        public async Task<IActionResult> SendResetPasswordEmail(string email)
        {
            var model = new ConfrimEmailRequest
            {
                Email = email,
                Code = await GenerateCode()
            };
            TempData["ResetPasswordCode"] = model.Code;
            var result = await authService.SendResetPassowrdEmail(model);
            return Ok();
        }
        public async Task<IActionResult> CheckResetP(ConfrimEmailRequest model)
        {
            var correctCode = Convert.ToInt32(TempData.Peek("ResetPasswordCode"));
            if (model.Code == correctCode)
                TempData.Remove("ResetPasswordCode");
            return Ok();
        }
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.ResetPasswordAsync(model);
                if (result.IsSucceded)
                {

                }
            }
            return Ok();
        }
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            return Ok();
        }
        public async Task<IActionResult> SaveChangePassword(ChangePasswordRequest model)
        {
            model.Email = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)!.Value;
            return Ok();
        }
        #endregion

        #region Login & Logout
        [HttpGet]
        public async Task<IActionResult> ShowLogin()
        {
            return View("login" , new LoginRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.LoginAsync(model);
                if (result.IsSucceded)
                {
                    
                    return RedirectToAction("HomeIndex", "HomeArea", new { area = "Home" });
                }
                else
                {
                    if (result.Message.Contains("Password"))
                        ModelState.AddModelError("Password", result.Message);
                    else
                        ModelState.AddModelError("Email", result.Message);
                }
            }
            return View("Login" , model);
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction("HomeIndex", "HomeArea", new {area="Home"});
        }
        #endregion




        private async Task<int> GenerateCode()
        {
            Random code = new Random();
            return code.Next(100000, 999999);
        }
    }
}
