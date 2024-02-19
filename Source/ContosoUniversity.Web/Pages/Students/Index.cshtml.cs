using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models;

namespace ContosoUniversity.Web.Pages.Students;

public class IndexModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public IList<Student> Student { get; set; } = default!;

    public async Task OnGetAsync() => Student = await _context.Students.ToListAsync();
}
