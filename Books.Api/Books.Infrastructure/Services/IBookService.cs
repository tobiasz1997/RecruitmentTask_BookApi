namespace Books.Infrastructure.Services
{
    using DTO;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<BookDto> GetAsync(Guid id);
        Task<IEnumerable<BookDto>> GetAllAsync();

        Task AddBookAsync(Guid id, string title, string author, string category, string publishingCompany,
                          string description, int pages);

        Task UpdateBookAsync(Guid id, string description);
        Task DeleteBookAsync(Guid id);
    }
}
