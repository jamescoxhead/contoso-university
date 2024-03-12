using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Web.Models.Entities;

public class Instructor
{
    public int InstructorId { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(50)]
    public required string LastName { get; set; }

    [Required]
    [Column("FirstName")]
    [Display(Name = "First Name")]
    [StringLength(50)]
    public required string FirstMidName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Hire Date")]
    public DateTime HireDate { get; set; }

    [Display(Name = "Full Name")]
    public string FullName => LastName + ", " + FirstMidName;

    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public OfficeAssignment? OfficeAssignment { get; set; } = default!;
}
