using Books.Common.Options;
using Books.Core.Domain;
using Books.MySqlDatabase.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Books.MySqlDatabase
{
    public class BooksContext : DbContext
    {
        private readonly DatabaseOptions _databaseOptions;

        public DbSet<Book> Books { get; set; }


        public BooksContext(IOptions<DatabaseOptions> options)
        {
            _databaseOptions = options.Value;  
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseOptions.ConnectionString);
        }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
