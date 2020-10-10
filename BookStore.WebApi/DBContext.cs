using BookStore.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Books> Books { get; set; }
    }
}
