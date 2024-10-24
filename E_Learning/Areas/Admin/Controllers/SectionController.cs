using E_Learning.Areas.Admin.Models;
using E_Learning.Models;
using E_Learning.Repositories.Repository;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Core.Types;
using static System.Collections.Specialized.BitVector32;

namespace E_Learning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SectionController : Controller
    {
        private readonly ICourseSectionRepository _section;
        private readonly ISectionLessonRepository lessonRepository;

        public SectionController(ICourseSectionRepository sectionRep, ISectionLessonRepository lessonRepository) 
        {
            this._section = sectionRep;
            this.lessonRepository = lessonRepository;
        }

        //private   SectionListViewModel Convert(IEnumerable<CourseSection> Sections , string CourseId) 
        //{
        //    var sectionDataTasks = Sections.Select( s => new SectionListViewModel
        //    {
        //        CourseId = CourseId,
        //        Id = s.Id,
        //        order = s.order,
        //        Title = s.Title
        //    });   
        //    return  sectionDataTasks;
        //}

        [HttpGet]
        public async Task<IActionResult> Index(string Id)
        {
            var Sections =  await _section.GetSectionsByCourseIdAsync(Id);
            ViewBag.CourseId = Id;
            //var list = await Convert(Sections , Id);
            return View(Sections);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            var section = new SectionViewModel { CourseId = id };
            return View(section);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SectionViewModel section)
        {
            if (ModelState.IsValid)
            {
                var newsection =
                    new CourseSection
                    {
                        Id = section.Id,
                        CourseId = section.CourseId,
                        order = section.order,
                        Title = section.Title
                    };
                try
                {
                    await _section.AddAsync(newsection);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                if (section.AddLesson)
                {
                    //Redurict to add lessons page
                    return RedirectToAction("Create", "Lesson", section.Id);
                }
                return RedirectToAction("Create" ,"Section", section.CourseId);
            }

            return View(section);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var section = await _section.GetByIdAsync(id);

            var newsection = new SectionViewModel 
            {  CourseId = section.CourseId,
               Id = id,
               order = section.order,
               Title = section.Title
            };

            return View(newsection);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(SectionViewModel section)
        {
            if (ModelState.IsValid)
            {
                var oldsection = await _section.GetByIdAsync(section.Id);
                var newsection =
                    new CourseSection
                    {
                        Id = section.Id,
                        CourseId = section.CourseId,
                        order = section.order,
                        Title = section.Title
                    };
                try
                {
                    await _section.UpdateAsync(oldsection, newsection);
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
                return RedirectToAction("Index", "Section", new { Id = section.CourseId });
            }

            return View(section);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var course = await _section.GetByIdAsync(id);
             
                await _section.DeleteAsync(id); 
                
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
