using AutoMapper;
using Books.Core.Domain;
using Books.Core.Repositories;
using Books.Infrastructure.DTO;
using Books.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDTO> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);

            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Category = book.Category,
                PublishingCompany = book.PublishingCompany,
                Description = book.Description,
                Pages = book.Pages
            };
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task AddBookAsync(Guid id, string title, string author, string category, string publishingCompany,
                                 string description, int pages)
        {
            var book = await _bookRepository.GetAsync(id);
            if (book != null)
            {
                throw new Exception($"Book {id} is exist");
            }
            book = new Book(id, title, author, category, publishingCompany, description, pages);
            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(Guid id, string description)
        {
            var book = await _bookRepository.GetOrFailAsync(id);
            book.SetDescription(description);
            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(Guid id)
        {
            var book = await _bookRepository.GetOrFailAsync(id);
            await _bookRepository.DeleteBookAsync(book);
        }
    }
}
