using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ContosoUniversity.Web.Pages.Instructors;

public class CreateModel(SchoolDbContext context, ILogger<CreateModel> logger) : BaseInstructorPageModel
{
    private readonly SchoolDbContext _context = context;
    private readonly ILogger<CreateModel> _logger = logger;

    [BindProperty]
    public Instructor Instructor { get; set; } = default!;

    public IActionResult OnGet()
    {
        var instructor = new Instructor
        {
            FirstMidName = string.Empty,
            LastName = string.Empty,
            Courses = new List<Course>()
        };

        PopulateAssignedCourseData(_context, instructor);

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string[] selectedCourses)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var createInstructor = new Instructor { FirstMidName = string.Empty, LastName = string.Empty };

        if (selectedCourses.Length > 0)
        {
            createInstructor.Courses = new List<Course>();
            _context.Courses.Load();
        }

        foreach (var course in selectedCourses)
        {
            var foundCourse = await _context.Courses.FindAsync(int.Parse(course, CultureInfo.InvariantCulture));
            if (foundCourse != null)
            {
                createInstructor.Courses.Add(foundCourse);
            }
            else
            {
                _logger.LogWarning("Course {Course} not found", course);
            }
        }

        try
        {
            if (await TryUpdateModelAsync(createInstructor,
                                          "Instructor",
                                          i => i.FirstMidName,
                                          i => i.LastName,
                                          i => i.HireDate,
                                          i => i.OfficeAssignment))
            {
                _context.Instructors.Add(createInstructor);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not create instructor");
        }

        PopulateAssignedCourseData(_context, createInstructor);
        return Page();
    }
}
