using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<EntriesList> EntriesLists { get; set; } = null!;
        public DbSet<Entry> Entries { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
