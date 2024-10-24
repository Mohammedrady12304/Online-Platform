using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface IQuizQuestionRepository : IRepository<QuizQuestion>
    {
        Task<IEnumerable<QuizQuestion>> GetQuestionsByQuizIdAsync(string quizId);
    }

}
