using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class SectionQuiz
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string  Title { get; set; }
        public int TotalMarks { get; set; }
        public List<QuizQuestion> QuizQuestions { get; set; }
        [ForeignKey("CourseSection")]
        public string SectionId { get; set; }
        public CourseSection CourseSection { get; set; }
    }
}
