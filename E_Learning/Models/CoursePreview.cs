using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{    
   public class CoursePreview
   {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = null!;
		//[Url(ErrorMessage = "this is not valid URL")]
		public string Videourl { get; set; } = null!;
        [ForeignKey(nameof(Course))]
        public string CourseId { get; set; }
        public Course? course { get; set; }
   }
    
}
