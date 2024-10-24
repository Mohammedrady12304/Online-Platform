using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsController(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactUs model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",model);
            }
            _contactUsRepository.add(model);


            return RedirectToAction(nameof(Success));

        }

    }
}
