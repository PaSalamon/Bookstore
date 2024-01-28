using Bookstore.Models;

namespace Bookstore.DbServices
{
    public interface IBookService
    {
        Task<BookModel> AddBook(BookModel book);

        Task<List<BookModel>> GetAllBooks();

        Task<BookModel?> GetBookByID(Guid id);

        Task<BookModel?> RemoveBook(Guid id);

        Task<BookModel> EditBook(BookModel book);


    }
}
