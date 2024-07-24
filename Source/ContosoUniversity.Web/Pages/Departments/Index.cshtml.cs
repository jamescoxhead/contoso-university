using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Web.Pages.Departments;

public class IndexModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public IList<Department> Department { get;set; } = default!;

    public async Task OnGetAsync() => Department = await _context.Departments
            .Include(d => d.Administrator).ToListAsync();
}
