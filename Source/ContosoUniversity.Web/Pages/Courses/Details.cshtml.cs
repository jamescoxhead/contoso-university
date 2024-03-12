using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Models.Entities;
using ContosoUniversity.Web.Data;

namespace ContosoUniversity.Web.Pages.Courses;

public class DetailsModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public Course Course { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.AsNoTracking()
                                           .Include(c => c.Department)
                                           .FirstOrDefaultAsync(m => m.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }
        else
        {
            Course = course;
        }

        return Page();
    }
}
