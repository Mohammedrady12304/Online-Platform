using E_Learning.Areas.Course.Data.Repositories;
using E_Learning.Areas.Course.Models;
using E_Learning.Areas.Home.Data;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repository.IReposatories;

namespace E_Learning.Areas.Course.Data.Services
{
	public class CourseFullDataViewModelService : ICourseFullDataViewModelService
	{
		private readonly ICourseViewRepository courseView;
		private readonly ICourseReviewRepository courseReview;
		private readonly ICourseSectionRepository courseSection;
		private readonly IUserDataShortcutService userData;
		private readonly ICourseCardService courseCard;

		public CourseFullDataViewModelService(ICourseViewRepository courseView , ICourseReviewRepository courseReview
											,ICourseSectionRepository courseSection , IUserDataShortcutService userData 
											, ICourseCardService courseCard) 
		{
			this.courseView = courseView;
			this.courseReview = courseReview;
			this.courseSection = courseSection;
			this.userData = userData;
			this.courseCard = courseCard;
		}

		public Task<CourseFullDataViewModel> GetFullDataByIdAsync(string Id)
		{
			CourseFullDataViewModel cs = new();
			cs.CourseView = this.courseView.GetByIdAsync(Id)?.Result!;
            cs.UserDataShortcutView = this.userData.GetDatabyid(cs.CourseView.InstructorId).Result;
            cs.Review = this.courseReview.GetCourseReviews(Id).ToList();
			cs.Sections = this.courseSection.GetSectionsByCourseIdLazyAsync(Id).Result.ToList();
			cs.CourseCardDetails = this.courseCard.getAll().Result.Take(4).ToList();
			return Task.FromResult(cs);
		}
	}
}
