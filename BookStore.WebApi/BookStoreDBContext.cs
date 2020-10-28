using BookStore.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebAPI
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> context) : base(context)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
