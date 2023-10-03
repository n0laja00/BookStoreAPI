using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Policy;

namespace BookStoreAPI.Utilities
{
    public class QueryUtilities
    {
        /// <summary>
        /// Query to filter by author
        /// </summary>
        /// <param name="query"></param>
        /// <param name="author"></param>
        /// <returns>Query that filters by author</returns>
        public static IQueryable<Book> FilterByAuthor(IQueryable<Book> query, string? author)
        {
            if (author != null)
            {
                if (string.IsNullOrWhiteSpace(author))
                {
                    HttpResponseUtilities.ThrowHttpException("Author can't be empty", HttpStatusCode.BadRequest);
                }
                query = query.Where(e => e.Author.ToLower() == author.ToLower());
            } 
            return query;
        }

        /// <summary>
        /// Query to filter by year
        /// </summary>
        /// <param name="query"></param>
        /// <param name="author"></param>
        /// <returns>Query that filters by year</returns>
        public static IQueryable<Book> FilterByYear(IQueryable<Book> query, int? year)
        {
            if (year != null)
            {
                query = query.Where(e => e.Year == year);
            } 

            return query;
        }

        /// <summary>
        /// Query to filter by author
        /// </summary>
        /// <param name="query"></param>
        /// <param name="author"></param>
        /// <returns>Query that filters by year</returns>
        public static IQueryable<Book> FilterByPublisher(IQueryable<Book> query, string? publisher)
        {
            if (publisher != null)
            {
                if (string.IsNullOrWhiteSpace(publisher))
                {
                    HttpResponseUtilities.ThrowHttpException("Publisher can't be empty", HttpStatusCode.BadRequest);
                }
                query = query.Where(e => e.Publisher.ToLower() == publisher.ToLower());
            }
             
            return query;
        }

        /// <summary>
        /// Send a query to database 
        /// </summary>
        /// <param name="title">Title of the book</param>
        /// <param name="author">The name of the author</param>
        /// <param name="year"> Year published</param>
        /// <returns>True if the book exists. False if it doesn't.</returns>
        public static bool BookExistsInDatabase(DataContext DataContext, string title, string author, int? year=0)
        {
            return (DataContext.Book?.Any(e => e.Author == author && e.Title == title && e.Year == year)).GetValueOrDefault();
        }
    }
}
