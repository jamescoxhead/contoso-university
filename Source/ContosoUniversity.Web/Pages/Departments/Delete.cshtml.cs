using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models.Entities;

namespace ContosoUniversity.Web.Pages.Departments;

public class DeleteModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    [BindProperty]
    public Department? Department { get; set; } = default!;
    public string ConcurrencyErrorMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int id, bool? concurrencyError)
    {
        Department = await _context.Departments.Include(d => d.Administrator)
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync(m => m.DepartmentId == id);

        if (Department == null)
        {
            return NotFound();
        }

        if (concurrencyError.GetValueOrDefault())
        {
            ConcurrencyErrorMessage = "The record you attempted to delete "
              + "was modified by another user after you selected delete. "
              + "The delete operation was canceled and the current values in the "
              + "database have been displayed. If you still want to delete this "
              + "record, click the Delete button again.";
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (await _context.Departments.AnyAsync(m => m.DepartmentId == id))
            {
                _context.Departments.Remove(Department!);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        catch (DbUpdateConcurrencyException)
        {
            return RedirectToPage("./Delete", new { concurrencyError = true, id });
        }
    }
}
