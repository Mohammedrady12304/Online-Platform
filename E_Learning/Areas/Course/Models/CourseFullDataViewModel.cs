using E_Learning.Areas.Home.Models;
using E_Learning.Models;

namespace E_Learning.Areas.Course.Models
{
	public class CourseFullDataViewModel
	{
        public List<CourseReviewView> Review { get; set; } = null!;

        public CourseView CourseView { get; set; } = null!;

        public List<CourseSection>? Sections { get; set; } = null!;

        public UserDataShortcutViewModel UserDataShortcutView { get; set; } = null!;
        
        public List<CourseCardDetails> CourseCardDetails { get; set; } = null!;

    }
}
