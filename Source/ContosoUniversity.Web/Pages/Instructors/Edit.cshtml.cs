using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.Web.Data;
using System.Globalization;

namespace ContosoUniversity.Web.Pages.Instructors;

public class EditModel(SchoolDbContext context) : BaseInstructorPageModel
{
    private readonly SchoolDbContext _context = context;

    [BindProperty]
    public Instructor Instructor { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors.Include(i => i.OfficeAssignment)
                                                   .Include(i => i.Courses)
                                                   .AsNoTracking()
                                                   .FirstOrDefaultAsync(m => m.InstructorId == id);

        if (instructor == null)
        {
            return NotFound();
        }

        PopulateAssignedCourseData(_context, instructor);
        Instructor = instructor;

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (id == null)
        {
            return NotFound();
        }

        var instructorToUpdate = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(s => s.InstructorId == id);

        if (instructorToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync(instructorToUpdate,
                                      "Instructor",
                                      i => i.FirstMidName,
                                      i => i.LastName,
                                      i => i.HireDate,
                                      i => i.OfficeAssignment))
        {
            if (string.IsNullOrWhiteSpace(
                instructorToUpdate.OfficeAssignment?.Location))
            {
                instructorToUpdate.OfficeAssignment = null;
            }

            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        UpdateInstructorCourses(selectedCourses, instructorToUpdate);
        PopulateAssignedCourseData(_context, instructorToUpdate);

        return Page();
    }

    private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
    {
        if (selectedCourses == null)
        {
            instructorToUpdate.Courses = new List<Course>();
            return;
        }

        var selectedCoursesHS = new HashSet<string>(selectedCourses);
        var instructorCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseId));

        foreach (var course in _context.Courses)
        {
            if (selectedCoursesHS.Contains(course.CourseId.ToString(CultureInfo.InvariantCulture)))
            {
                if (!instructorCourses.Contains(course.CourseId))
                {
                    instructorToUpdate.Courses.Add(course);
                }
            }
            else
            {
                if (instructorCourses.Contains(course.CourseId))
                {
                    var courseToRemove = instructorToUpdate.Courses.Single(
                                                    c => c.CourseId == course.CourseId);
                    instructorToUpdate.Courses.Remove(courseToRemove);
                }
            }
        }
    }
}
