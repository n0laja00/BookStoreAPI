using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAPI.Data;
using BookStoreAPI.Models;
using System.Net;
using Newtonsoft.Json;
using BookStoreAPI.Helpers;
using BookStoreAPI.Utilities;

namespace BookStoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Book
        /// <summary>
        /// We search for all the books as default. if there are queries, we take them into account and perform our search.
        /// </summary>
        /// <param name="author">string author</param>
        /// <param name="year">int year</param>
        /// <param name="publisher">string publisher</param>
        /// <returns>Json:All books or some books that fulfil query param conditions. Exception is thrown at a failed operation.</returns>
        [HttpGet("")]
#nullable enable
        public async Task<BookList> GetBook(string? author = null, int? year = null, string? publisher = null)
        {

            BookList response = new BookList();

            if (_context.Book == null)
            {
                throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("Not found. Something went wrong with data retrieval.",
                    HttpStatusCode.NotFound));
            }
            var query = _context.Book.AsQueryable();

            query = QueryUtilities.FilterByAuthor(query, author);
            query = QueryUtilities.FilterByYear(query, year);
            query = QueryUtilities.FilterByPublisher(query, publisher);

            response.Books = await query.ToListAsync();

            response.Books.Distinct().ToList();

            return response;
        }

        // GET: api/Book/5
        /// <summary>
        /// To get a specific book with ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Json: returns a book of specific ID. Exception is thrown at a failed operation.</returns>
        [HttpGet("{id}")]
        public async Task<Book> GetBook(int id)
        {
            if (_context.Book == null)
            {
                throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("Not Found. Something went wrong with data retrieval.",
                    HttpStatusCode.NotFound));
            }

            Book result = await _context.Book.FindAsync(id);

            if (result == null)
            {
                throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("Not found. The book doesn't exist.",
                    HttpStatusCode.NotFound));
            }

            return result;
        }



        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Receives a Post Request form a client and processes it. 
        /// </summary>
        /// <param name="book">Model: Book</param>
        /// <returns>Json: Id of the book just created in result. Exception is thrown at a failed operation.</returns>
        [HttpPost]
        public async Task<ActionResult<Object>> PostBook(Book book)
        {

            if (_context.Book == null)
            {
                throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("Not Found. Something went wrong with data retrieval.", HttpStatusCode.NotFound));
            }

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("Bad Request.", HttpStatusCode.BadRequest));
            }

            var context = _context;
            //Find a similar book in the database and then process it
            bool bookExists = QueryUtilities.BookExistsInDatabase(context, book.Title, book.Author, book.Year);
            if (!bookExists)
            {
                _context.Book.Add(book);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetBookModel", new { id = book.Id }, book);

                //Check that the book was truly created.
                if (book.Id != null)
                {
                    BookId returnId = new BookId();
                    returnId.Id = book.Id;
                    string result = JsonConvert.SerializeObject(returnId);
                    return result;
                }
                else
                {
                    throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("Internal Server Error. Something went wrong with book creation.",
                        HttpStatusCode.InternalServerError));
                }
            }
            else
            {
                throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("Bad Request. The book already exists.",
                    HttpStatusCode.BadRequest));
            };
        }

        // DELETE: api/Book/5
        /// <summary>
        /// Delete book based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns No Content if successful. Exception is thrown at a failed operation.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Object>> DeleteBook(int id)
        {
            if (_context.Book == null)
            {
                return HttpResponseUtilities.HttpResponseMessageMaker("Not found. Something went wrong with data retrieval.",
                    HttpStatusCode.NotFound);
            }

            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("Bad Request. The book already exists.",
                    HttpStatusCode.BadRequest));
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            throw new HttpResponseException(HttpResponseUtilities.HttpResponseMessageMaker("No content",
                HttpStatusCode.NoContent));
        }



    }
}
