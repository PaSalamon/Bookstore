using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DbServices
{
    public class BookService : IBookService

    {
        private readonly AppDbContext db;

        public BookService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<BookModel> AddBook(BookModel book)
        {
            await db.Books.AddAsync(book);
            await db.SaveChangesAsync();

            return book;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await db.Books.ToListAsync();
        }

        public async Task<BookModel?> GetBookByID(Guid id)
        {
            return await db.Books.FindAsync(id);

        }

        public async Task<BookModel?> RemoveBook(Guid id)
        {
            var book = await db.Books.FindAsync(id);
            if (book != null)
            {
                db.Books.Remove(book);
            }
            
            await db.SaveChangesAsync();

            return book;
        }

        public async Task<BookModel> EditBook(BookModel book)
        {
            db.Update(book);
            await db.SaveChangesAsync();

            return book;
        }
    }
}
