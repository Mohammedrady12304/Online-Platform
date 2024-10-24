namespace E_Learning.Areas.Admin.Models
{
    public class ChangeImageViewModel
    {
        public string CourseId { get; set; } = null!;
        
        public string? ImageUrl { get; set; }

        public IFormFile? Image { get; set; }


    }
}
