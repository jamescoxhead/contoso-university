namespace ContosoUniversity.Web.Models;

public class Student
{
    public int StudentId { get; set; }
    public required string LastName { get; set; }
    public required string FirstMidName { get; set; }
    public required DateTime EnrollmentDate { get; set; }

    public ICollection<Enrollment>? Enrollments { get; set; }
}
