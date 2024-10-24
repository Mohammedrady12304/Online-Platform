namespace E_Learning.Areas.Payment.Models
{
    public class CourseSummaryViewModel
    {
        public double  total { get; set; }

        public Dictionary<string,int> CourseIdMony { get; set; }

       

        public string UserId { get; set; }

        public CourseSummaryViewModel()
        { 
            total = 0.0;
            CourseIdMony = new Dictionary<string, int>();
            this.UserId = null!;
        }

    }
}
