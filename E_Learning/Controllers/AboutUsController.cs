using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly IAboutUsRepository _aboutUsRepository;

        public AboutUsController(IAboutUsRepository aboutUsRepository)
        {
                _aboutUsRepository = aboutUsRepository;
        }

        
        public IActionResult Index()
        {
            var aboutUs = _aboutUsRepository.GetAll();
            return View(aboutUs);
        }
    }
}
