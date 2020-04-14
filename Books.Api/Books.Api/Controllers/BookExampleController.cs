namespace Books.Api.Controllers
{
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("book")]
    [ApiVersion("2.1")]
    public class BookExampleController : Controller
    {
        private readonly IBookService _bookService;

        public BookExampleController(IBookService bookService) => _bookService = bookService;


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
    }
}
