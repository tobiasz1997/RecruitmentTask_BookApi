namespace Books.Infrastructure.Services
{
    using AutoMapper;
    using Core.Domain;
    using Core.Repositories;
    using DTO;
    using Extensions;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }


        public async Task<BookDto> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);

            return new BookDto
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

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task AddBookAsync(Guid id, string title, string author, string category, string publishingCompany,
                                 string description, int pages)
        {
            var book = await _bookRepository.GetAsync(id);
            if (book != null)
                throw new Exception($"Book {id} is exist");

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