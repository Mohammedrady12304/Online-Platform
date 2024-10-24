namespace E_Learning.Areas.Course.Models
{
    public class CourseView
    {
        public string Id { get; set; }
        public string InstructorId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int totalHours { get; set; }
        //public string NumOfLessons { get; set; }
        public string level { get; set; }
        public int numOfStudents { get; set; }
        public string? CourseImage { get; set; }
        public string? trailerVideoUrl { get; set; }
        public int? numofSections { get; set; }
        public string SubCategoryId { get; set; }
		public string SubCategoryName { get; set; }


		public CourseView()
        {

        }
    }

}
