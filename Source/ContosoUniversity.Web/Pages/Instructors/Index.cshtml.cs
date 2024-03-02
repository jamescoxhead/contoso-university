using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models.ViewModels;

namespace ContosoUniversity.Web.Pages.Instructors;

public class IndexModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public InstructorIndexData InstructorData { get; set; } = new();
    public int InstructorId { get; set; }
    public int CourseId { get; set; }

    public async Task OnGetAsync(int? id, int? courseId)
    {
        InstructorData = new()
        {
            Instructors = await _context.Instructors.Include(i => i.OfficeAssignment)
                                                    .Include(i => i.Courses)
                                                        .ThenInclude(c => c.Department)
                                                    .OrderBy(i => i.LastName)
                                                    .ToListAsync()
        };

        if (id != null)
        {
            InstructorId = id.Value;
            var instructor = InstructorData.Instructors.Single(i => i.InstructorId == id.Value);
            InstructorData.Courses = instructor.Courses;
        }

        if (courseId != null)
        {
            courseId = courseId.Value;
            var enrollments = await _context.Enrollments.Where(e => e.CourseId == courseId)
                                                        .Include(i => i.Student)
                                                        .ToListAsync();
            InstructorData.Enrollments = enrollments;
        }
    }
}
