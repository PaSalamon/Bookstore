using Bookstore.DbServices;
using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly AppDbContext dbContext;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await bookService.GetAllBooks();

            return StatusCode(StatusCodes.Status200OK, books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByID(Guid id)
        {
            var book = await bookService.GetBookByID(id);

            return StatusCode(StatusCodes.Status200OK, book);
        }

        [HttpPost]
        public async Task<ActionResult<BookModel>> AddBook(BookModel book)
        {
            var dbBook = await bookService.AddBook(book);

            return StatusCode(StatusCodes.Status200OK, dbBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveBook(Guid id)
        {
            var dbBook = await bookService.RemoveBook(id);

            return StatusCode(StatusCodes.Status200OK, dbBook);
        }

        [HttpPut]
        public async Task<ActionResult> EditBook(BookModel book)
        {
            await bookService.EditBook(book);

            return StatusCode(StatusCodes.Status200OK, book);
        }
    }
}
