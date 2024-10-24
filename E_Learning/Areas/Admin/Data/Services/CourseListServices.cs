using E_Learning.Areas.Admin.Models;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Admin.Data.Services
{
    public class CourseListServices : ICourseListServices
    {
        private readonly ICourseRepository course;
        private IUserRepository User;
        private readonly ISubCategoryRepository subCategory;

        public CourseListServices(ICourseRepository course, IUserRepository user
                                  ,ISubCategoryRepository subCategory)
        {
            User = user;
            this.subCategory = subCategory;
            this.course = course;
        }


        private IEnumerable<CourseListViewModel> Convert(IEnumerable<E_Learning.Models.Course> courses)
        {
            return courses.
                Select(c =>
                new CourseListViewModel
                {
                    CreatedDate = c.CreatedDate,
                    Id = c.Id,
                    Image = c.Image,
                    instructorId = c.InstructorId,
                    Price = c.Price,
                    Rating = c.Rating,
                    registers = c.NumberOfRegisters,
                    Title = c.Title,
                    SubCategoryId = c.SubCategoryId,
                    instructorName = User.GetByIdAsync(c.InstructorId).Result.FName,
                    SubCategoryname = subCategory.GetByIdAsync(c.SubCategoryId).Result.Title,
                    status = c.Status
                });

        }

        public async Task<IEnumerable<CourseListViewModel>> GetAllCoursesAsync()
        {
            var courses = await course.GetAllAsync();
            var ListCourses = Convert(courses);
            return ListCourses;
        }

        public async Task<IEnumerable<CourseListViewModel>> GetCoursesBySubcategoryAsync(string SubcategoryId)
        {
            var courses = await course.GetCoursesBySubCategoryAsync(SubcategoryId);
            var ListCourses = Convert(courses);
            return ListCourses;
        }

        public async Task<IEnumerable<CourseListViewModel>> GetCoursesInPerioud(DateTime? startDate, DateTime? endDate)
        {
            var courses = await course.GetAllAsync();
            var result = courses.Where(c=>c.CreatedDate >= startDate
                                        && c.CreatedDate<=endDate).ToList();
            var ListCourses = Convert(result);
            return ListCourses;
        }

        public async Task<IEnumerable<CourseListViewModel>> GetCoursesForInstructorAsync(string InstructorId)
        {
            var courses = await course.GetAllAsync();
            var result = courses.Where(c=>c.InstructorId==InstructorId).ToList();
            var ListCourses = Convert(result);
            return ListCourses;
        }


    }
}
