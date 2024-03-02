using ContosoUniversity.Web.Models.Entities;

namespace ContosoUniversity.Web.Models.ViewModels;

public class InstructorIndexData
{
    public IEnumerable<Instructor> Instructors { get; set; } = Enumerable.Empty<Instructor>();
    public IEnumerable<Course> Courses { get; set; } = Enumerable.Empty<Course>();
    public IEnumerable<Enrollment> Enrollments { get; set; } = Enumerable.Empty<Enrollment>();
}
