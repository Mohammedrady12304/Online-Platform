using E_Learning.Areas.Admin.Models;
using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static System.Collections.Specialized.BitVector32;

namespace E_Learning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LessonController : Controller
    {
        private readonly ISectionLessonRepository _lesson;
        private readonly ICourseSectionRepository _section;

        public LessonController(ISectionLessonRepository lesson , ICourseSectionRepository _section)
        {
            this._lesson = lesson;
            this._section = _section;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var lessons = await _lesson.GetLessonsBySectionIdAsync(id);
            ViewBag.SectionId = id;
            //var list = await Convert(Sections , Id);
            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            var Lesson = new SectionLessons { SectionId = id };
            return View(Lesson);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SectionLessons lessons)
        {
            if (ModelState.IsValid)
            {
                var newlesson =
                    new SectionLessons
                    {
                        Id = lessons.Id,
                        SectionId = lessons.SectionId,
                        Order = lessons.Order,
                        Title = lessons.Title ,
                        AttachedFile = lessons.AttachedFile,
                        Videourl = lessons.Videourl
                    };
                try
                {
                    await _lesson.AddAsync(newlesson);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
               
                return RedirectToAction("Create", "Lesson", lessons.SectionId);
            }

            return View(lessons);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var lesson = await _lesson.GetByIdAsync(id);

            var section = await _section.GetByIdAsync(lesson.SectionId);

            var sections = await _section.GetSectionsByCourseIdAsync(section.CourseId);

            var sectionSelectedList = sections.Select(x => new SelectListItem { Value = x.Id, Text = x.Title });

            var LessonData = new LessonViewModel
            {
                SectionId = lesson.SectionId,
                Id = id,
                Order = lesson.Order,
                Title = lesson.Title,
                Videourl = lesson.Videourl,
                sections = sectionSelectedList,
                AttachedFile = lesson.AttachedFile
            };

            return View(LessonData);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(LessonViewModel lesson)
        {
            if (ModelState.IsValid)
            {
                var oldlesson = await _lesson.GetByIdAsync(lesson.Id);
                var newlesson =
                    new SectionLessons
                    {
                        Id = lesson.Id,
                        SectionId = lesson.SectionId,
                        Order = lesson.Order,
                        Title = lesson.Title,
                        Videourl = lesson.Videourl,
                        AttachedFile = lesson.AttachedFile
                    };
                try
                {
                    await _lesson.UpdateAsync(oldlesson, newlesson);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                //if (section.AddLesson)
                //{
                //    //Redirect to add lessons page
                //    return RedirectToAction("Create", "Lesson", section.Id);
                //}
                return RedirectToAction("Index", "Lesson", new { id = lesson.SectionId });
            }

            var section = await _section.GetByIdAsync(lesson.SectionId);

            var sections = await _section.GetSectionsByCourseIdAsync(section.CourseId);

            var sectionSelectedList = sections.Select(x => new SelectListItem { Value = x.Id, Text = x.Title });

            return View(lesson);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var course = await _lesson.GetByIdAsync(id);

                await _lesson.DeleteAsync(id);

                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
