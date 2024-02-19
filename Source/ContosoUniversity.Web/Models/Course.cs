using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Web.Models;

public class Course
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CourseId { get; set; }
    public required string Title { get; set; }
    public required int Credits { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
