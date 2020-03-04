using Books.Core.Domain;
using Books.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private static readonly ISet<Book> _books = new HashSet<Book>
        {
            new Book(Guid.NewGuid(), "Harry Potter", "J.K.Rowling","Fantasy","Media Family", "Nice book", 400),
            new Book(Guid.NewGuid(), "Book", "J.K.Rowling","Fiction","Media Family", "Nice books", 400),
            new Book(Guid.NewGuid(), "Harry Potter", "J.K.Rowling","Fantasy","Media Family", "Nice bookss", 400)
        };
        public async Task<Book> GetAsync(Guid id) => await Task.FromResult(_books.SingleOrDefault(x => x.Id == id));
        public async Task<IEnumerable<Book>> GetAllAsync() => await Task.FromResult(_books);
        public async Task AddBookAsync(Book book)
        {
            _books.Add(book);
            await Task.CompletedTask;
        }

        public async Task UpdateBookAsync(Book book)
        {
            _books.Where(x => x.Id == book.Id)
                .Select(x => book);
            await Task.CompletedTask;
        }


        public async Task DeleteBookAsync(Book book)
        {
            _books.Remove(book);
            await Task.CompletedTask;
        }

    }
}
