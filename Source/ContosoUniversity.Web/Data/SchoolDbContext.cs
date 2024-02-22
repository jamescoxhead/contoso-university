using ContosoUniversity.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Web.Data;

public class SchoolDbContext(DbContextOptions<SchoolDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Course> Courses { get; set; }
}
