using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Learning.Areas.Search.Data;
using System.Linq;
using System.Text;

using E_Learning.Models;

namespace E_Learning.Areas.Search.Controllers
{
    [Area("Search")]
	public class SearchController : Controller
	{
        
        private readonly ICourseSearchRepository _courseSearchRepository;

        public SearchController(ICourseSearchRepository courseSearchRepository)
        {
            _courseSearchRepository = courseSearchRepository;
        }

        public async Task<ActionResult> Search(string query, string category = null, 
            int? minRating = null, double? minPrice = null, double? maxPrice = null, 
            string language = null, string level = null, int pageNumber = 1)
        {
            const int pageSize = 10; // Set page size to 10

            // Use the repository to perform the search and paginate the results
            var searchResults = await _courseSearchRepository.SearchCoursesAsync(
                query, category, minRating, minPrice, maxPrice, language, level, pageNumber, pageSize
            );
            @ViewBag.Query = query;


            return View("Search",searchResults);
        }

    }
}
