using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Learning.Areas.Course.Controllers
{
    [Authorize] // Ensure that only logged-in users can access the wishlist
    public class WishListController : Controller
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly UserManager<User> _userManager;

        // Inject the WishListRepository and UserManager to handle user identity
        public WishListController(IWishListRepository wishListRepository, UserManager<User> userManager)
        {
            _wishListRepository = wishListRepository;
            _userManager = userManager;
        }

        // Helper method to get the current user's ID
        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id;
        }

        // GET: /WishList/
        // View all items in the wishlist for the current user
        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();
            var wishlistItems = await _wishListRepository.GetWishListsByUserIdAsync(userId);
            return View(wishlistItems); // You can create a corresponding view for this
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWishList(string courseId)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null || string.IsNullOrEmpty(courseId))
            {
                return BadRequest("Invalid user or course.");
            }

            WishList wish = new WishList { CourseId = courseId, UserId = userId };


            await _wishListRepository.AddAsync(wish);

            //if (!success)
            //{
            //    return BadRequest("Failed to add course to wishlist.");
            //}

            // Optionally return the updated wishlist count or status for the front-end
            var wishlistCount = await _wishListRepository.GetWishListsByUserIdAsync(userId);
            return Json(new { success = true, wishlistCount = wishlistCount });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromWishList(string courseId)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null || string.IsNullOrEmpty(courseId))
            {
                return BadRequest("Invalid user or course.");
            }

            var success = await _wishListRepository.DeleteAsync(userId, courseId);

            if (!success)
            {
                return BadRequest("Failed to remove course from wishlist.");
            }

            // Optionally return the updated wishlist count or status for the front-end
            var wishlistCount = await _wishListRepository.GetWishListsByUserIdAsync(userId);
            return Json(new { success = true, wishlistCount = wishlistCount });
        }



    }
}
