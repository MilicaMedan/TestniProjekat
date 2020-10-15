using Microsoft.EntityFrameworkCore;


namespace Backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }

        public DbSet<User.User> Users { get; set; }
        public DbSet<Priority.Priority> Priorities { get; set; }
        public DbSet<Status.Status> Statuses { get; set; }
        public DbSet<Taskk.Taskk> Tasks { get; set; }
    }
}
