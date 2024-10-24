using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
