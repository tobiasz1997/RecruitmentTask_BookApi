using Books.Infrastructure.Commands.Events;
using Books.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Controllers
{
    [Route("book")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookservice)
        {
            _bookService = bookservice;
        }

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
        public async Task<IActionResult> Post([FromBody]CreateBook command)
        {
            var newId = Guid.NewGuid();
            await _bookService.AddBookAsync(newId, command.Title, command.Author, command.Category,
                                            command.PublishingCompany, command.Description, command.Pages);

            return StatusCode(201, new { id = newId });
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> Put(Guid bookId, [FromBody]UpdateBook command)
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
