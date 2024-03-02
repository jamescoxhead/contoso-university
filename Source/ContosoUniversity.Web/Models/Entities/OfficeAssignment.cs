using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Web.Models.Entities;

public class OfficeAssignment
{
    [Key]
    public int InstructorId { get; set; }

    [StringLength(50)]
    [Display(Name = "Office Location")]
    public required string Location { get; set; }

    public Instructor Instructor { get; set; } = default!;
}
