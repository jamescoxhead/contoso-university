using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.Web.Data;

namespace ContosoUniversity.Web.Pages.Instructors;

public class DeleteModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    [BindProperty]
    public Instructor Instructor { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.InstructorId == id);

        if (instructor == null)
        {
            return NotFound();
        }
        else
        {
            Instructor = instructor;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Instructor instructor = await _context.Instructors
                .Include(i => i.Courses)
                .SingleAsync(i => i.InstructorId == id);

        if (instructor == null)
        {
            return RedirectToPage("./Index");
        }

        var departments = await _context.Departments
                .Where(d => d.InstructorId == id)
                .ToListAsync();
        departments.ForEach(d => d.InstructorId = null);

        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
