namespace BookStoreAPI.Interfaces
{
    public interface IBook
    {
        string Author { get; set; }
        string Description { get; set; }
        int? Id { get; set; }
        string Publisher { get; set; }
        string Title { get; set; }
        int? Year { get; set; }
    }
}