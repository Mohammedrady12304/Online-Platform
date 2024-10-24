using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
