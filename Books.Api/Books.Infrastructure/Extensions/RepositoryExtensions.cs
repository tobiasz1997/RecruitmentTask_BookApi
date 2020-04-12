namespace Books.Infrastructure.Extensions
{
    using Core.Repositories;
    using System;
    using Core.Domain;
    using System.Threading.Tasks;

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