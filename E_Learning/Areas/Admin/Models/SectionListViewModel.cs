namespace E_Learning.Areas.Admin.Models
{
    public class SectionListViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int order { get; set; }
        public int NumberOfVideos { get; set; }
        public string CourseId { get; set; }
    }
}
