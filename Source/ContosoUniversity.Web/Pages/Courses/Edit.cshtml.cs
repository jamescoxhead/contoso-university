using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Models.Entities;
using ContosoUniversity.Web.Data;

namespace ContosoUniversity.Web.Pages.Courses;

public class EditModel(SchoolDbContext context) : BaseCoursePageModel
{
    private readonly SchoolDbContext _context = context;

    [BindProperty]
    public Course Course { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.Include(c => c.Department).FirstOrDefaultAsync(m => m.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }

        PopulateDepartmentSelectList(_context, course.DepartmentId);

        Course = course;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (!ModelState.IsValid)
        {
            PopulateDepartmentSelectList(_context);
            return Page();
        }

        var updateCourse = await _context.Courses.FindAsync(id);

        if (updateCourse == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync(
                 updateCourse,
                 "course",
                   c => c.Credits, c => c.DepartmentId, c => c.Title))
        {
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        PopulateDepartmentSelectList(_context, updateCourse.DepartmentId);
        return Page();
    }
}
