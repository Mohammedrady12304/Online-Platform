using E_Learning.Helper;
using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repositories.Repository;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Security.Claims;

namespace E_Learning.Areas.Payment.Controllers
{
    [Authorize]
    [Area("Payment")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        private readonly ICourseRepository _courseRepository;
        private readonly UserManager<User> _userManager;
        private readonly IViewRenderService view;

        public CartController(ICartRepository cartRepository,
                              ICourseRepository courseRepository , 
                              UserManager<User> userManager,
                              IViewRenderService view)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            this.view = view;
            _courseRepository = courseRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId(); // Get the logged-in user's ID
            if (userId == null)
            {
                return RedirectToAction("ShowLogin", "Account", new { area = "Authentication" });
            }

            var carts = await _cartRepository.GetCartsByUserIdAsync(userId);
            return View(carts);
        }

        // Retrieve the logged-in user's ID
        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(User); // Get the logged-in user's ID
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(string courseId)
        {
            var userId =  GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }


          
            var existingCart = await _cartRepository.GetByIdAsync(courseId ,  userId);
            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    CourseId = courseId,
                    UserId = userId
                };
                await _cartRepository.AddAsync(newCart);
               
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Course already in cart." });
        }

      
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string courseId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            try
            {
                // Remove the course from the cart
                await _cartRepository.DeleteAsync(courseId, userId);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Error Please try again" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> deletefromcart(string courseid)
        {
            var userid = GetCurrentUserId();

            if (userid == null)
            {
                return RedirectToAction("login", "account"); // redirect to login if not authenticated
            }

            var cartitem = await _cartRepository.GetByIdAsync(courseid, userid);
            if (cartitem == null)
            {
                return NotFound();
            }

            await _cartRepository.DeleteAsync(courseid, userid);

            // after deleting, return the updated cart items as a partial view
            var updatedcartitems = await _cartRepository.GetCartsByUserIdAsync(userid);
           // ViewBag.cartsummary = PartialView("_cartsummarypartialview", updatedcartitems);
            return PartialView("_cartitemspartialview", updatedcartitems);
        }




    }
}

