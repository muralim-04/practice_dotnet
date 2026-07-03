using Microsoft.EntityFrameworkCore;
using practice_dotnet.Entities;

namespace practice_dotnet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

    }
}
