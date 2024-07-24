using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.Web.Data;

namespace ContosoUniversity.Web.Pages.Courses;

public class IndexModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public IList<Course> Courses { get; set; } = default!;

    public async Task OnGetAsync() => Courses = await _context.Courses.Include(c => c.Department).AsNoTracking().ToListAsync();
}
