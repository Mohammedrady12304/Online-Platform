namespace E_Learning.Areas.Admin.Models
{
    public class CourseListViewModel
    {
        public string Id { get; set; }
        public string? Image { get; set; }
        public string Title { get; set; }
        public string instructorName { get; set; }
        public double Price { get; set; }
        public string instructorId { get; set; }
        public int registers { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryname { get; set; }
        public int? Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public string status { get; set; }

    }
}
