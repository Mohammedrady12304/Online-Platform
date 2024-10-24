namespace E_Learning.Models
{
    public class AboutUs
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? Vision { get; set; }
        public string? Mission { get; set; }
        public string? ContactEmail { get; set; } 
        public string? ContactPhone { get; set; } 
        public string? FacebookLink { get; set; } 
        public string? LinkedInLink { get; set; } 
        public string? TwitterLink { get; set; }  
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }


}
