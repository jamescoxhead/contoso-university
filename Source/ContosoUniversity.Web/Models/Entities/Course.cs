using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Web.Models.Entities;

public class Course
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Number")]
    public int CourseId { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public required string Title { get; set; }

    [Range(0, 5)]
    public int Credits { get; set; }

    public int DepartmentId { get; set; }

    public Department Department { get; set; } = default!;
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
}
