using Books.Core.Domain;
using Books.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Book> GetOrFailAsync(this IBookRepository repository, Guid id)
        {
            var book = await repository.GetAsync(id);
            if (book == null)
            {
                throw new Exception($"Book {id} is not exist!");
            }

            return book;
        }
    }
}
