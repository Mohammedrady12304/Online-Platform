using E_Learning.Areas.Course.Data.Services;
using Microsoft.AspNetCore.Mvc;
using E_Learning.Models;
using E_Learning.Areas.Payment.Models;
using System.Security.Claims;
using E_Learning.Repository.IReposatories;
using E_Learning.Repositories.Repository;

namespace E_Learning.Areas.Course.Controllers
{
	[Area("Course")]
	public class CourseController : Controller
	{
		private readonly ICourseFullDataViewModelService courseFullData;
        private readonly ICourseRepository _course;

        public CourseController(ICourseFullDataViewModelService courseFullData,ICourseRepository course )
        {
			this.courseFullData = courseFullData;
            _course = course;
        }

        public IActionResult Index(string id)
		{
			var data  = courseFullData.GetFullDataByIdAsync(id).Result;
			var numofLessons =  from s in data.Sections
							    select s.SectionLessons.Count;

			ViewBag.NumberOfLessons = numofLessons.Sum() ;


			return View(data);
		}

		public async Task<IActionResult> PayCourse(string Id) 
		{
			if(ModelState.IsValid)
			{
                E_Learning.Models.Course course = await _course.GetByIdAsync(Id);


                var x = new Dictionary<string, int>();
				x[course.Id]=(int)course.Price;
                CourseSummaryViewModel model = new CourseSummaryViewModel()
				{
					total = course.Price,
					UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
					CourseIdMony = x
				};

				return View(model);
			}

			return BadRequest();
		}

        [HttpGet]
        public IActionResult ViewCourse(string Id)
        {
            var course = _course.GetById(Id);
            return View("ViewCourse", course);
        }

    }
}
