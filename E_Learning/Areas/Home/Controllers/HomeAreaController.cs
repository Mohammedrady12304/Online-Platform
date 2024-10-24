using E_Learning.Areas.Home.Data;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeAreaController : Controller
    {
        //ICourseCardService _courseCardService;
        //public HomeAreaController(ICourseCardService courseCardService)
        //{
        //    _courseCardService = courseCardService;
        //}

        ICourseRepository _courseCardService;
        public HomeAreaController(ICourseRepository courseCardService)
        {
            _courseCardService = courseCardService;
        }

        public async Task<IActionResult> HomeIndex()
        {
            var ccs = await _courseCardService.GetAllAsync();
            return View("Area_Index", ccs);
        }
    }
}
