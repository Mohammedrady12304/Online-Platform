using E_Learning.Areas.Admin.Data.Services;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Learning.Areas.Admin.Models;
using E_Learning.Models;

namespace E_Learning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly ICourseLevelRepository _courseLevelRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICourseListServices courseList;
        private readonly ICoursePreviewRepository _coursePreview;
        private readonly ICourseDiscountRepository _descount;

        public CoursesController(ICourseLevelRepository courseLevelRepository, 
            ISubCategoryRepository subCategoryRepository
            ,ILanguageRepository languageRepository,
            ICourseRepository courseRepository,
            IStatusRepository statusRepository,
            IWebHostEnvironment webHostEnvironment,
            ICourseListServices courseList,
            ICourseDiscountRepository _descount,
            ICoursePreviewRepository coursePreview
            )
            
        {
            _courseLevelRepository = courseLevelRepository;
            _subCategoryRepository = subCategoryRepository;
            this.languageRepository = languageRepository;
            this.courseRepository = courseRepository;
            this.statusRepository = statusRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.courseList = courseList;
            this._coursePreview = coursePreview;
            this._descount = _descount;
        }

        public async Task<IActionResult> Index()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CoursesList(string userId)
        {
            var courses = await courseList.GetCoursesForInstructorAsync(userId);
            return View("CoursesList" , courses);
        }

        [HttpGet]
        public async Task<IActionResult> CoursesList()
        {
            var courses = await courseList.GetAllCoursesAsync();
            return View("CoursesList", courses);
        }

        private async Task fillCourse(Models.CourseViewModel model)
        {
            var courseLevels = await _courseLevelRepository.GetAllAsync();
            var subCategories = await _subCategoryRepository.GetAllAsync();
            var languages = await languageRepository.GetAllAsync();
            var status = await statusRepository.GetAllStatusesAsync();

            model.CourseLevels = courseLevels.Select(cl => new SelectListItem { Value = cl.NameId, Text = cl.NameId });
            model.SubCategories = subCategories.Select(sc => new SelectListItem { Value = sc.Id, Text = sc.Title });
            model.Languages = languages.Select(la => new SelectListItem { Value = la.NameId, Text = la.NameId });
            model.StatusList = status.Select(st => new SelectListItem { Value = st.NameId, Text = st.NameId });
           
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new Models.CourseViewModel();
            await fillCourse(model);
            return View(model);        
        }


        [HttpPost]
        public async Task<IActionResult> Create(Models.CourseViewModel model )
        {
            if (ModelState.IsValid)
            {
                // Create a new course object
                var course = new E_Learning.Models.Course
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = model.Title,
                    Description = model.Description,
                    Duration = model.Duration,
                    Summary = model.Summary,
                    Status = model.Status,
                    Price = model.Price,
                    CreatedDate = model.CreatedDate,
                    CourseLevel = model.SelectedCourseLevel,
                    SubCategoryId = model.SubCategoryId,
                    InstructorId = model.InstructorId!,
                    Language = model.SelectedLanguage
                };

               
                // Handle image upload
                if (model.Image != null && model.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "Courses");

                    // Ensure the directory exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Create the file path using the CourseId as the name
                    var fileName = $"{course.Id}{Path.GetExtension(model.Image.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Save the file to the target location
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }

                    // Save the image path in the course entity
                    course.Image = fileName;
                }
                // Save the course data
                await courseRepository.AddAsync(course);

                if (model.AddSection)
                {
                    return RedirectToAction("Create" , "Section" , model.Id);
                }
                else
                {
                    return RedirectToAction("Index");
                }
              
            }
           
            await fillCourse(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id )
        {
            if (ModelState.IsValid) 
            { 
                var course = await courseRepository.GetByIdAsync(id);
                var CourseView = new CourseViewModel
                {

                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Duration = course.Duration,
                    Summary = course.Summary,
                    Status = course.Status,
                    Price = course.Price,
                    CreatedDate = course.CreatedDate,
                    SelectedCourseLevel = course.CourseLevel,
                    SubCategoryId = course.SubCategoryId,
                    InstructorId = course.InstructorId!,
                    SelectedLanguage = course.Language,
                    Rating = course.Rating
                };
                ViewBag.Image = course.Image;
                await fillCourse(CourseView);
                return View(CourseView);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new course object
                var oldCourse = courseRepository.GetById(model.Id!);

                var course = new E_Learning.Models.Course
                {
                    Id = oldCourse.Id,
                    Title = model.Title ,
                    Description = model.Description,
                    Duration = model.Duration,
                    Summary = model.Summary,
                    Status = model.Status,
                    Price = model.Price,
                    CreatedDate = model.CreatedDate,
                    CourseLevel = model.SelectedCourseLevel,
                    SubCategoryId = model.SubCategoryId,
                    InstructorId = model.InstructorId!,
                    Language = model.SelectedLanguage,
                    Image = oldCourse.Image,
                    Rating = oldCourse.Rating,
                    NumberOfRegisters = oldCourse.NumberOfRegisters,  
                };

                // Handle image upload if a new image is provided
                if (model.Image != null && model.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "Courses");

                    // Ensure the directory exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Create the file path using the CourseId as the name
                    var fileName = $"{course.Id}{Path.GetExtension(model.Image.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(course.Image))
                    {
                        var oldImagePath = Path.Combine(uploadsFolder, course.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save the new image to the target location
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }

                    // Save the new image path in the course entity
                    course.Image = fileName;
                }
                else
                {
                    // If no new image is uploaded, retain the existing image path
                    course.Image = course.Image;
                }

                // Save the course data
                await courseRepository.UpdateAsync(oldCourse, course);
                return RedirectToAction("CoursesList");
            }

            return BadRequest();
        }

      //  [Route("course/details/{id}", Name = "CourseDetails")]
        public async Task<IActionResult> Detail(string id)
        {
            if (ModelState.IsValid)
            {
                var course = await courseRepository.GetByIdAsync(id);
                if (course != null)
                {
                 //   course.courseSections  ;

                }

                return View(course);
            }

            return RedirectToAction("CoursesList");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var course = courseRepository.GetById(id);
                // Your logic to delete the course from the databas
                await courseRepository.DeleteAsync(id); // Placeholder for your delete method

                // Path to the image file in wwwroot (assuming it's stored in "wwwroot/Images/Courses/")
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Courses", course.Image); // Replace ImageFileName with the actual property

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath); // Delete the image file
                }

                return Json(new { success = true });
                   
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        //public async Task<IActionResult> Search()
        //{

        //}
        [HttpGet]
        public async Task<IActionResult> ChangeImage(string id)
        {
            var Course = await courseRepository.GetByIdAsync(id);
            var image = new ChangeImageViewModel
            {
                ImageUrl = Course.Image,
                CourseId = id
            };
            return View(image);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeImage(ChangeImageViewModel changed)
        {
            var course = await courseRepository.GetByIdAsync(changed.CourseId);

            if (changed.Image != null && changed.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "Courses");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Create the file path using the CourseId as the name
                var fileName = $"{changed.CourseId}{Path.GetExtension(changed.Image.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(course.Image))
                {
                    var oldImagePath = Path.Combine(uploadsFolder, course.Image);
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
                course.Image = fileName;
            }
            else
            {
                // If no new image is uploaded, retain the existing image path
                course.Image = course.Image;
            }

            // Save the course data
            try
            {

                await courseRepository.UpdateAsync(course, course);
                return RedirectToAction("ChangeImage", "Courses", new { id = course.Id });
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpGet]
        public async Task<IActionResult> Trailer(string Id)
        {
            var trailerVideo = await _coursePreview.GetPreviewsByCourseIdAsync(Id);
            if (trailerVideo == null)
            {
                trailerVideo = new CoursePreview
                {
                    CourseId = Id
                };
        
            }
            return View(trailerVideo);
        }

        [HttpPost]
        public async Task<IActionResult> Trailer(CoursePreview trailer)
        {
            if (ModelState.IsValid) 
            {
                var Oldtrailer = await _coursePreview.GetPreviewsByCourseIdAsync(trailer.Id);
                if(Oldtrailer == null)
                {
                    try
                    {
                        await _coursePreview.AddAsync(trailer);
                        return View(trailer);
                    }
                    catch
                    {
                        return View(Oldtrailer);
                    }
                }
                else
                {
                    try
                    {
                        _coursePreview.UpdateAsync(Oldtrailer, trailer);
                        return View(trailer);
                    }
                    catch
                    {
                        return View(Oldtrailer);
                    }
                }
            }

            return View(trailer);
        }


        public async Task<IActionResult> Discount(string Id)
        {
            var Discount = await _descount.GetDiscountsByCourseIdAsync(Id);
            if (Discount == null) 
            {
                return View(new CourseDiscount());
            }

            return View(Discount);
        }


    }
}

