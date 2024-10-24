using E_Learning.Repositories.IReposatories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_Learning.Models;
namespace E_Learning.Areas.Student.Controllers
{
    [Area("Student")]
    public class LearningController : Controller
    {
        private readonly IStudentCourseProgressRepository _progressRepository;
        private readonly UserManager<User> _userManager; 
        public LearningController(IStudentCourseProgressRepository progressRepository , 
            UserManager<User> userManager)
        {
            _progressRepository = progressRepository;
            _userManager = userManager;
        }
        // Retrieve the logged-in user's ID
        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(User)!; // Get the logged-in user's ID
        }
        public IActionResult Progresses()
        {
            var studentID = GetCurrentUserId();
            if (studentID == null)
            {
                return Unauthorized();
            }

            var studentProgress = _progressRepository.GetProgressByStudent(studentID);

            return View(studentProgress);
        }
    }
}
