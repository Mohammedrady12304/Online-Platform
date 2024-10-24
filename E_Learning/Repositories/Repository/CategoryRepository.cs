using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Repositories.Repository
{
    //public class CategoryRepository : ICategoryRepository
    //{
    //    private readonly ApplicationDbContext _context;

    //    public CategoryRepository(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<IEnumerable<Category>> GetAllAsync()
    //    {
    //        var list = await _context.Categories.ToListAsync();
    //        return list;
    //    }

    //    public async Task<Category> GetByIdAsync(string id)
    //    {
    //        var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
    //        return category;
    //    }

    //    public async Task AddAsync(Category category)
    //    {
    //        await _context.Categories.AddAsync(category);
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task UpdateAsync(Category New , Category Old)
    //    {
    //        Old.Name = New.Name;
    //        _context.Update(Old);
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task DeleteAsync(string id)
    //    {
    //        var category = await GetByIdAsync(id);
    //        if (category != null)
    //        {
    //            _context.Categories.Remove(category);
    //            await _context.SaveChangesAsync();
    //        }
    //    }

    //    public async Task<Category> GetCourseFromCategories(string categoryId)
    //    {
    //        var course = await _context.Categories.Include(c => c.Courses)
    //            .FirstOrDefaultAsync(c => c.Id == categoryId);
    //        return course!;
    //    }

  
    //}

}
