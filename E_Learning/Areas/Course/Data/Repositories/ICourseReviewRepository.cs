using E_Learning.Areas.Course.Models;

namespace E_Learning.Areas.Course.Data.Repositories
{
	public interface ICourseReviewRepository
	{
		IEnumerable<CourseReviewView> GetCourseReviews(string courseId);
	}
}
