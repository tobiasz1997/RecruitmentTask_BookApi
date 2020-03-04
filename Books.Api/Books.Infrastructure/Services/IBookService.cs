using Books.Core.Domain;
using Books.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Services
{
    public interface IBookService
    {
        Task<BookDTO> GetAsync(Guid id);
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task AddBookAsync(Guid id, string title, string author, string category, string publishingCompany,
                          string description, int pages);
        Task UpdateBookAsync(Guid id, string description);
        Task DeleteBookAsync(Guid id);
    }
}
