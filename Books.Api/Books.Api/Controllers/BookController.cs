using Microsoft.Web.Http;

namespace Books.Api.Controllers
{
    using Infrastructure.Commands.Events;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("book")]
    [ApiVersion("1.7")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) => _bookService = bookService;


        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllAsync();
            if (books == null)
            {
                return NotFound();
            }

            return Json(books);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> Get(Guid bookId)
        {
            var book = await _bookService.GetAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            return Json(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBook command)
        {
            var newId = Guid.NewGuid();
            await _bookService.AddBookAsync(newId, command.Title, command.Author, command.Category,
                                            command.PublishingCompany, command.Description, command.Pages);

            return StatusCode(201, new {id = newId});
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> Put(Guid bookId, [FromBody] UpdateBook command)
        {
            await _bookService.UpdateBookAsync(bookId, command.Description);
            return NoContent(); //204
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Delete(Guid bookId)
        {
            await _bookService.DeleteBookAsync(bookId);
            return NoContent(); //204
        }
    }
}