using Microsoft.EntityFrameworkCore;

namespace DuluxBrief.Server.Entities
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .IsRequired();
            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .IsRequired();
        }
    }
}