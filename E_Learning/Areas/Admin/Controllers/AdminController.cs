using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_Learning.Models;
using E_Learning.Areas.Admin.Models;
namespace E_Learning.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View("Index");
        }

        public async Task<IActionResult> Users()
        {
            return View("Users");
        }

        public async Task<IActionResult> AdminList()
        {
            // Find all users in the "Admin" role
            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            // Map the list to AdminViewModel
            var adminViewModels = admins.Select(admin => new AdminViewModel
            {
                Username = admin.UserName,
                Email = admin.Email,
                FullName = admin.FName + " " + admin.LName,
                CreatedDate = admin.DateJoined
            }).ToList();

            return View("AdminList", adminViewModels);
        }

        // [Authorize(Roles ="Admin")]
        public IActionResult AssignAdmin()
        {
            return View(new AssignAdminViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignAdminRole(AssignAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, "Admin");
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = $"{model.Username} has been assigned the Admin role.";
                        return RedirectToAction("AssignAdmin");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View("AssignAdmin", model);
        }




        // [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveAdminRole(string id)
        {
            var user = await _userManager.FindByNameAsync(id);
            if (user != null)
            {
                // Check if the user is in the Admin role
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
                    if (result.Succeeded)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error removing admin role." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "User is not an admin." });
                }
            }
            return Json(new { success = false, message = "User not found." });
        }
    }

}