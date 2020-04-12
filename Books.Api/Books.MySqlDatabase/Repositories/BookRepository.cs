namespace Books.Database.Repositories
{
    using Books.Core.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Core.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MySqlDatabase;

    public class BookRepository : IBookRepository
    {
        private readonly BooksContext _booksContext;


        public BookRepository(BooksContext booksContext) => _booksContext = booksContext;

        public async Task<Book> GetAsync(Guid id) => await _booksContext.Books.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Book>> GetAllAsync() => await _booksContext.Books.AsNoTracking().ToListAsync();

        public async Task AddBookAsync(Book book)
        {
            await _booksContext.Books.AddAsync(book);
            await _booksContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _booksContext.Books.Update(book);
            await _booksContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
            _booksContext.Books.Remove(book);
            await _booksContext.SaveChangesAsync();
        }
    }
}