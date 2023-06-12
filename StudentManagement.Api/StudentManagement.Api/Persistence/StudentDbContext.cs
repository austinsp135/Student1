using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Model;

namespace StudentManagement.Api.Persistence
{
    public class StudentDbContext: DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Mark> Marks { get; set; }
    }
}
