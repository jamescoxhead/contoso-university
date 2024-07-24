using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Domain.Entities;

public class Student
{
    public int StudentId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public required string LastName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
    [Column("FirstName")]
    [Display(Name = "First Name")]
    public required string FirstMidName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Enrollment Date")]
    public required DateTime EnrollmentDate { get; set; }

    [Display(Name = "Full Name")]
    public string FullName => LastName + ", " + FirstMidName;

    public ICollection<Enrollment> Enrollments { get; set; } = [];
}
