using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class QuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizQuestion>> GetAllAsync()
        {
            return await _context.Set<QuizQuestion>().ToListAsync();
        }

        public async Task<QuizQuestion> GetByIdAsync(string id)
        {
            return await _context.Set<QuizQuestion>().FindAsync(id);
        }

        public async Task AddAsync(QuizQuestion quizQuestion )
        {
            await _context.Set<QuizQuestion>().AddAsync(quizQuestion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuizQuestion Old, QuizQuestion New)
        {
            {
                Old.Text = New.Text;
                Old.Choices = New.Choices;
                Old.Order = New.Order;
                Old.correctAnswer = New.correctAnswer;
            }
            _context.Set<QuizQuestion>().Update(Old);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var quizQuestion = await _context.Set<QuizQuestion>().FindAsync(id);
            if (quizQuestion != null)
            {
                _context.Set<QuizQuestion>().Remove(quizQuestion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<QuizQuestion>> GetQuestionsByQuizIdAsync(string quizId)
        {
            return await _context.Set<QuizQuestion>()
                .Where(q => q.QuizId == quizId)
                .ToListAsync();
        }
    }

}
