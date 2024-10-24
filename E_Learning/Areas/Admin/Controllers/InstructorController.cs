using E_Learning.Areas.Admin.Models;
using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private UserManager<User> _userManager;
        private readonly IDataForInstructorRepository _instructordata;
        private readonly IUserRepository _userRepository;

        public InstructorController(UserManager<User> userManager, IDataForInstructorRepository Instructordata, IUserRepository userRepository)
        {
            _userManager = userManager;
            _instructordata = Instructordata;
            this._userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> InstructorList()
        {
            var instructors = await _userManager.GetUsersInRoleAsync("Instructor");
            var instructorViewModels = instructors.Select(instructor =>
            {
                var data = _instructordata.GetInstructorDataByInstructorIdAsync(instructor.Id).Result;
                return new InstructorViewModel
                {
                    Username = instructor.UserName,
                    Email = instructor.Email,
                    FullName = instructor.FName + " " + instructor.LName,
                    JoinDate = instructor.DateJoined,
                    Balance = data.Balance,
                    Profession = data.Profession
                };
            }).ToList();
            return View(instructorViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> AssignInstructor()
        {
            return View(new AssignInstructorViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignInstructor(AssignInstructorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetByUserNameAsync(model.InstructorUsername);
                if (user != null)
                {
                    if (!_userManager.GetUsersInRoleAsync("Instructor").Result.Contains(user))
                    {
                        var instructor = new DataForInstructor
                        {
                            UserId = user.Id,
                        };
                        try
                        {
                            await _instructordata.AddInstructorDataAsync(instructor);
                            var result = await _userManager.AddToRoleAsync(user, "Instructor");
                            if (result.Succeeded)
                            {
                                TempData["SuccessMessage"] = $"{user.UserName} has been assigned the Instructor role.";
                                return RedirectToAction("AssignInstructor");
                            }
                            else
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError(string.Empty, error.Description);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, "Error in operations");
                        }

                        return View(new AssignInstructorViewModel());
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RemoveInstructorRole(string id)
        {
            var user = await _userManager.FindByNameAsync(id);
            if (user != null)
            {
                // Check if the user is in the Admin role
                if (await _userManager.IsInRoleAsync(user, "Instructor"))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "Instructor");
                    await _instructordata.DeleteInstructorDataAsync(id);
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
