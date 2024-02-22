using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models.Entities;

namespace ContosoUniversity.Web.Pages.Students;

public class CreateModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public IActionResult OnGet() => Page();

    [BindProperty]
    public Student Student { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var createStudent = new Student { EnrollmentDate = DateTime.Now, FirstMidName = string.Empty, LastName = string.Empty };

        if (await TryUpdateModelAsync(createStudent, "student", s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
        {
            _context.Students.Add(createStudent);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        return Page();
    }
}
