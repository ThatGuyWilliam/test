using Microsoft.EntityFrameworkCore;
using SkillTest.Models;

namespace SkillTest
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Task { get; set; }
    }
}