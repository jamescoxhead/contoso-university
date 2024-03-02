using ContosoUniversity.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Web.Data;

public class SchoolDbContext(DbContextOptions<SchoolDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasMany(c => c.Instructors).WithMany(i => i.Courses);

        base.OnModelCreating(modelBuilder);
    }
}
