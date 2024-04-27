using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Web.Pages;

public class AboutModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    public IList<EnrollmentDateGroup> EnrollmentData { get; set; } = [];

    public async Task OnGetAsync()
    {
        var enrollmentData = from student in _context.Students
                             group student by student.EnrollmentDate into dateGroup
                             select new EnrollmentDateGroup
                             {
                                 EnrollmentDate = dateGroup.Key,
                                 StudentCount = dateGroup.Count()
                             };

        EnrollmentData = await enrollmentData.AsNoTracking().ToListAsync();
    }
}
