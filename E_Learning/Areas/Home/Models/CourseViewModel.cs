

using E_Learning.Models;

namespace E_Learning.Areas.Home.Models
{
    public class CourseViewModel
    {
        public IEnumerable<E_Learning.Models.Course> Courses { get; set; }

        public IEnumerable<Cart> CartCourses { get; set; }

    }
}
