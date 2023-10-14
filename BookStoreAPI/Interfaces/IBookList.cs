using BookStoreAPI.Models;

namespace BookStoreAPI.Interfaces
{
    public interface IBookList
    {
        List<Book> Books { get; set; }
    }
}