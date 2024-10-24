using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Repository
{
    public class SectionLessonRepository : ISectionLessonRepository
    {
        private readonly ApplicationDbContext _context;

        public SectionLessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SectionLessons>> GetAllAsync()
        {
            return await _context.Set<SectionLessons>().ToListAsync();
        }

        public async Task<SectionLessons> GetByIdAsync(string id)
        {
            return await _context.Set<SectionLessons>().FindAsync(id);
        }

        public async Task AddAsync(SectionLessons sectionLesson)
        {
            await _context.Set<SectionLessons>().AddAsync(sectionLesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SectionLessons Lesson1,SectionLessons Lesson2)
        {
            {
                Lesson1.Title = Lesson2.Title;
                Lesson1.Videourl = Lesson2.Videourl;
                Lesson1.AttachedFile = Lesson2.AttachedFile;
                Lesson1.Order = Lesson2.Order;
                Lesson1.SectionId = Lesson2.SectionId;
            }

            _context.Set<SectionLessons>().Update(Lesson1);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var sectionLesson = await _context.Set<SectionLessons>().FindAsync(id);
            if (sectionLesson != null)
            {
                _context.Set<SectionLessons>().Remove(sectionLesson);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SectionLessons>> GetLessonsBySectionIdAsync(string sectionId)
        {
            return await _context.Set<SectionLessons>()
                .Where(lesson => lesson.SectionId == sectionId)
                .ToListAsync();
        }
    }

}
