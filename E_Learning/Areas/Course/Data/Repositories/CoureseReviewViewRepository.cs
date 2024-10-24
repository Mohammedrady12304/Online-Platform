using E_Learning.Areas.Course.Models;
using E_Learning.Models;

namespace E_Learning.Areas.Course.Data.Repositories
{
	public class CourseReviewRepository : ICourseReviewRepository
	{
		private readonly ApplicationDbContext _context;

		public CourseReviewRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<CourseReviewView> GetCourseReviews(string courseId)
		{
			// Query the view from the database and map it to the ViewModel
			var courseReviews = _context.courseReviewViewModels
				.Where(cr => cr.CourseId == courseId)
				.Select(cr => new CourseReviewView
				{
					CourseId = cr.CourseId,
					Review = cr.Review,
					ReviewerName = cr.ReviewerName,
					ReviewerImg = cr.ReviewerImg,
					ReviewerRating = cr.ReviewerRating
				})
				.ToList();

			return courseReviews;
		}
	}

}
