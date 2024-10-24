using System.Linq;
using System.Data;

using E_Learning.Areas.Search.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Areas.Search.Data
{
    public static class  SearchService
    {

        public static string PreprocessQuery(string query, string[] stopWords)
        {
            var words = query.Split(' ')
                             .Where(word => !stopWords.Contains(word.ToLower()))  // Remove stop words
                             .ToList();

            // TODO: Add synonym handling here (e.g., replace 'course' with 'class', 'lesson')
            return string.Join(" ", words);
        }

        public static List<CourseSearchViewModel> ApplyFuzzyMatching(List<CourseSearchViewModel> results, string query)
        {
            return results.Where(c => CalculateLevenshteinDistance(c.Title, query) <= 2
                                   || CalculateLevenshteinDistance(c.Description, query) <= 2).ToList();
        }

        public static List<CourseSearchViewModel> RankResults(List<CourseSearchViewModel> results, string query)
        {
            // Rank based on the occurrence of the query in Title (more weight) and Description
            return results.OrderByDescending(c =>
                (c.Title.Contains(query) ? 3 : 0) +  // Highest weight for Title matches
                (c.Description.Contains(query) ? 2 : 0) +  // Moderate weight for Description
                (c.InstructorName.Contains(query) ? 1 : 0))  // Lowest weight for Instructor Name
                .ToList();
        }

        public static int CalculateLevenshteinDistance(string source, string target)
        {
            if (source.Length == 0) return target.Length;
            if (target.Length == 0) return source.Length;

            int[,] distance = new int[source.Length + 1, target.Length + 1];

            for (int i = 0; i <= source.Length; distance[i, 0] = i++) { }
            for (int j = 0; j <= target.Length; distance[0, j] = j++) { }

            for (int i = 1; i <= source.Length; i++)
            {
                for (int j = 1; j <= target.Length; j++)
                {
                    int cost = target[j - 1] == source[i - 1] ? 0 : 1;
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[source.Length, target.Length];
        }
    }



}

