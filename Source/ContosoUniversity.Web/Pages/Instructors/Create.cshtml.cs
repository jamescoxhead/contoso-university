using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoUniversity.Web.Models.Entities;
using ContosoUniversity.Web.Data;

namespace ContosoUniversity.Web.Pages.Instructors;

public class CreateModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public IActionResult OnGet() => Page();

    [BindProperty]
    public Instructor Instructor { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Instructors.Add(Instructor);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
