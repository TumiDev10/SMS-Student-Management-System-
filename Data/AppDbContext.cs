using Microsoft.EntityFrameworkCore;
using SMS_Student_Management_System_.Models;

namespace SMS_Student_Management_System_.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<ParentGuardian> ParentsGuardians { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, constraints, etc.
            modelBuilder.Entity<ParentGuardian>()
                .HasKey(pg => new { pg.ParentId, pg.StudentId });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}