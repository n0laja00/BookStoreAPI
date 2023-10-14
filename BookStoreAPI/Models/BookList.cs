using BookStoreAPI.Interfaces;

namespace BookStoreAPI.Models
{
    public class BookList : IBookList
    {

        public List<Book>? Books { get; set; }

    }
}
