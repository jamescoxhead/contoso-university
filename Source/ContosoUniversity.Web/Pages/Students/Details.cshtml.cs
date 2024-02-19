using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models;

namespace ContosoUniversity.Web.Pages.Students;

public class DetailsModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public Student Student { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.Include(student => student.Enrollments)
                                             .ThenInclude(enrollment => enrollment.Course)
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(m => m.StudentId == id);
        if (student == null)
        {
            return NotFound();
        }
        else
        {
            Student = student;
        }

        return Page();
    }
}
