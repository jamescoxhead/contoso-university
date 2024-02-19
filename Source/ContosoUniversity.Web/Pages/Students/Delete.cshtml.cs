using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models;

namespace ContosoUniversity.Web.Pages.Students;

public class DeleteModel(SchoolDbContext context, ILogger<DeleteModel> logger) : PageModel
{
    private readonly SchoolDbContext _context = context;
    private readonly ILogger<DeleteModel> _logger = logger;

    [BindProperty]
    public Student Student { get; set; } = default!;
    public string ErrorMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.AsNoTracking()
                                             .FirstOrDefaultAsync(m => m.StudentId == id);

        if (student == null)
        {
            return NotFound();
        }
        else
        {
            Student = student;
        }

        if (saveChangesError.GetValueOrDefault())
        {
            ErrorMessage = $"Delete {id} failed. Try again";
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
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

        try
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Delete {Id} failed. Try again", id);
            return RedirectToAction("./Delete", new { id, saveChangesError = true });
        }
    }
}
