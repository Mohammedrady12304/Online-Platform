using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class Review
    {
        public string Id {  get; set; }
        public string FeedBack { get; set; }
        public int Evaluation { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User")]    
        public string UserId { get; set; }
        [ForeignKey("Course")]
        public string CorurseId { get; set; }
        public User User { get; set; }
        public Course Course { get; set; }
    }
}
