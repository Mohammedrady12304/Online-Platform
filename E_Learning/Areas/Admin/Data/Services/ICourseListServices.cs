using E_Learning.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Admin.Data.Services
{
    public interface ICourseListServices
    {
        Task<IEnumerable<CourseListViewModel>> GetAllCoursesAsync();
        Task<IEnumerable<CourseListViewModel>> GetCoursesForInstructorAsync(string InstructorId);
        Task<IEnumerable<CourseListViewModel>> GetCoursesBySubcategoryAsync(string SubcategoryId);
        Task<IEnumerable<CourseListViewModel>> GetCoursesInPerioud(DateTime? startDate, DateTime? endDate);
    }
}
