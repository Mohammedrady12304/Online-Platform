using E_Learning.Repository.IReposatories;
using E_Learning.Areas.Home.Models;
using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.AspNetCore.Identity;

namespace E_Learning.Areas.Home.Data
{
    public class CourseCardService:ICourseCardService
    {
        private readonly ICourseRepository _course;
		private readonly ICourseDiscountRepository courseDiscount;
        private readonly ICartRepository cart;
        private readonly IWishListRepository wishList;
        private readonly UserManager<User> _userManager;

        public CourseCardService(ICourseRepository course , 
                                 ICourseDiscountRepository courseDiscount)
        {
            _course = course;
			this.courseDiscount = courseDiscount;
        }

       


        public  async Task<CourseCardDetails> getCourseAsync(string id)
        {
            CourseCardDetails details = new CourseCardDetails();
            var course = _course.GetByIdAsync(id);

            details.Id = id;
            details.NumberOfRegisters = course.Result.NumberOfRegisters;
            details.Title = course.Result.Title;
            details.Description = course.Result.Description;
            details.Price = course.Result.Price;
            details.Image = course.Result.Image;
            details.Summary = course.Result.Summary;


            var descount = await courseDiscount.GetByIdAsync(id);
            if (descount != null)
            {
                details.netPrice = course.Result.Price - course.Result.Price * descount.Percentage;
            }
            else
            {
                details.netPrice = course.Result.Price;

			}
            return details;
        }

        public async Task<IEnumerable<CourseCardDetails>> getAll() 
        { 
            var IDS = _course.GetAllAsync().Result.Select(c => c.Id).ToList();

            List<CourseCardDetails> c = new List<CourseCardDetails>();
            foreach (var id in IDS) 
            { 
                c.Add(await getCourseAsync(id));
            }

            return c;
        }


    }
}
