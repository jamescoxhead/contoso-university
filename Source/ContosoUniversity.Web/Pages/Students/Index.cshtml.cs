using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models.Entities;
using ContosoUniversity.Web.Models.ViewModels;
using ContosoUniversity.Web.ExtensionMethods;

namespace ContosoUniversity.Web.Pages.Students;

public class IndexModel(SchoolDbContext context, IConfiguration configuration) : PageModel
{
    private readonly SchoolDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    public string NameSort { get; set; } = string.Empty;
    public string DateSort { get; set; } = string.Empty;
    public string CurrentFilter { get; set; } = string.Empty;
    public string CurrentSort { get; set; } = string.Empty;

    public PaginatedList<Student> Students { get; set; } = new([], 0, 1, 1);

    public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;
        NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
        DateSort = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToLowerInvariant().Equals("date", StringComparison.OrdinalIgnoreCase) ? "date_desc" : "Date";
        CurrentFilter = searchString;

        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        var studentsQuery = from s in _context.Students
                            select s;

        if (!string.IsNullOrEmpty(searchString))
        {
            studentsQuery = studentsQuery.Where(s => s.LastName.Contains(searchString) ||
                                                     s.FirstMidName.Contains(searchString));
        }

        studentsQuery = sortOrder switch
        {
            "name_desc" => studentsQuery.OrderByDescending(s => s.LastName),
            "Date" => studentsQuery.OrderBy(s => s.EnrollmentDate),
            "date_desc" => studentsQuery.OrderByDescending(s => s.EnrollmentDate),
            _ => studentsQuery.OrderBy(s => s.LastName),
        };

        var pageSize = _configuration.GetValue("ContosoUniversity:PageSize", 4);
        Students = await studentsQuery.AsNoTracking().ToPaginatedListAsync(pageIndex ?? 1, pageSize);
    }
}
