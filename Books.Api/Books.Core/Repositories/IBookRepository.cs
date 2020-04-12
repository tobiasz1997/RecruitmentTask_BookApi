namespace Books.Core.Repositories
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookRepository
    {
        Task<Book> GetAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
    }
}