using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Web.Pages.Departments;

public class CreateModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public IActionResult OnGet()
    {
    ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "FirstMidName");
        return Page();
    }

    [BindProperty]
    public Department Department { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Departments.Add(Department);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
