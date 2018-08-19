using CookNShareWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CookNShareWebApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        DbSet<User> Users { get; set; }
    }
}