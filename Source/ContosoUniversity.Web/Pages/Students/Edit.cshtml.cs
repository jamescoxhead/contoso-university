using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models;

namespace ContosoUniversity.Web.Pages.Students;

public class EditModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    [BindProperty]
    public Student Student { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        Student = student;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updateStudent = await _context.Students.FindAsync(id);

        if (updateStudent == null)
        {
            return NotFound();
        }

        try
        {
            if (await TryUpdateModelAsync(updateStudent, "student", s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(Student.StudentId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool StudentExists(int id) => _context.Students.Any(e => e.StudentId == id);
}
