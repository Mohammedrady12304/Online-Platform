using E_Learning.Areas.Admin.Models;
using E_Learning.Repositories.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace E_Learning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GeneralController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IDataForInstructorRepository _DataRepo;
        public GeneralController(IUserRepository user , IWebHostEnvironment webHostEnvironment
                                , IDataForInstructorRepository _DataRepo)
        {
            _userRepository = user;
            this.webHostEnvironment = webHostEnvironment;
            this._DataRepo = _DataRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangeImage(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var image = new ChangeImageViewModel
            {
                ImageUrl = user.Image,
                CourseId = id
            };
            return View(image);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeImage(ChangeImageViewModel changed)
        {
            var NormalUser = await _userRepository.GetByIdAsync(changed.CourseId);

            if (changed.Image != null && changed.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "Users");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Create the file path using the CourseId as the name
                var fileName = $"{changed.CourseId}{Path.GetExtension(changed.Image.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(NormalUser.Image) && !NormalUser.Image.Contains("NotDef"))
                {
                    var oldImagePath = Path.Combine(uploadsFolder, NormalUser.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Save the new image to the target location
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await changed.Image.CopyToAsync(fileStream);
                }

                // Save the new image path in the course entity
                NormalUser.Image = fileName;
            }
            else
            {
                // If no new image is uploaded, retain the existing image path
                NormalUser.Image = NormalUser.Image;
            }

            // Save the course data
            try
            {
                await _userRepository.UpdateAsync(NormalUser, NormalUser);
                return RedirectToAction("ChangeImage", "General", new { id = NormalUser.Id });
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var data = await _DataRepo.GetInstructorDataByInstructorIdAsync(userId);
            if (data == null)
            {
                data = new DataForInstructor { UserId = userId  };
            }

            return View(data);
        }

        [HttpPost]
        public async  Task<IActionResult> Edit(DataForInstructor model)
        {
            if (ModelState.IsValid)
            {
                var existingData = await _DataRepo.GetInstructorDataByInstructorIdAsync(model.UserId);


                if (existingData != null)
                {
                    // Update existing data
                    existingData.Profession = model.Profession;
                    existingData.Bio = model.Bio;
                    existingData.Balance = model.Balance;

                }
                else
                {
                    // Add new data
                    await _DataRepo.AddInstructorDataAsync(model);
                }

                ViewBag.SuccessMessage = "Data has been successfully saved!";
            

                return View(model); 
            }

                return View(model); 
        }

    }
}
