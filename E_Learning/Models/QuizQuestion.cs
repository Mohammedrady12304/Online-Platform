using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class QuizQuestion
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; }
        public int Order { get; set; }
        public List<string> Choices { get; set; }
        public string correctAnswer { get; set; }
        [ForeignKey("SectionQuiz")]
        public string QuizId { get; set; }
        public SectionQuiz SectionQuiz { get; set; }
    }
}
