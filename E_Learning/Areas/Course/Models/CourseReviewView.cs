using System.ComponentModel.DataAnnotations;

namespace E_Learning.Areas.Course.Models
{
	// it is view in database
	public class CourseReviewView
	{
		public string CourseId { get; set; }
		public string Review { get; set; }
		public string ReviewerName { get; set; }
		public string ReviewerImg { get; set; }
		[Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
		public int ReviewerRating { get; set; }

	}
}
