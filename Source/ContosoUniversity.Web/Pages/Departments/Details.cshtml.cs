using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Web.Pages.Departments;

public class DetailsModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public Department Department { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentId == id);
        if (department == null)
        {
            return NotFound();
        }
        else
        {
            Department = department;
        }

        return Page();
    }
}
