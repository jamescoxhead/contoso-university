using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Models.Entities;
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

        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor != null)
        {
            Instructor = instructor;
            _context.Instructors.Remove(Instructor);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
