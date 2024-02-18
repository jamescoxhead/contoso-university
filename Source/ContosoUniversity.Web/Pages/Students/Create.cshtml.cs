using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoUniversity.Web.Models;
using ContosoUniversity.Web.Data;

namespace ContosoUniversity.Web.Pages.Students;

public class CreateModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public IActionResult OnGet() => Page();

    [BindProperty]
    public Student Student { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Students.Add(Student);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
