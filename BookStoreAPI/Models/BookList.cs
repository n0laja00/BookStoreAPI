using BookStoreAPI.Interfaces;

namespace BookStoreAPI.Models
{
    /// <summary>
    /// Lists books
    /// </summary>
    public class BookList : IBookList
    {

        public List<Book>? Books { get; set; }

    }
}
