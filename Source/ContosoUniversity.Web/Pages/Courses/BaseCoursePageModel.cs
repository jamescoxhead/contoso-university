using ContosoUniversity.Web.Data;
using ContosoUniversity.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Web.Pages.Courses;

public abstract class BaseCoursePageModel : PageModel
{
    public required SelectList DepartmentNames { get; set; }

    public void PopulateDepartmentSelectList(SchoolDbContext context, object? selectedDepartment = null)
    {
        var departments = from d in context.Departments
                          orderby d.Name
                          select d;

        DepartmentNames = new SelectList(departments.AsNoTracking(),
                                         nameof(Department.DepartmentId),
                                         nameof(Department.Name),
                                         selectedDepartment);
    }
}
