using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Web.Data;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Web.Pages.Courses;

public class CreateModel(SchoolDbContext context) : BaseCoursePageModel
{
    private readonly SchoolDbContext _context = context;

    public IActionResult OnGet()
    {
        PopulateDepartmentSelectList(_context);
        return Page();
    }

    [BindProperty]
    public Course Course { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateDepartmentSelectList(_context);
            return Page();
        }

        var createCourse = new Course { Title = string.Empty };

        if (await TryUpdateModelAsync(createCourse,
                                      "course",
                                      s => s.CourseId,
                                      s => s.DepartmentId,
                                      s => s.Title,
                                      s => s.Credits))
        {
            _context.Courses.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        PopulateDepartmentSelectList(_context, createCourse.DepartmentId);
        return Page();
    }
}
