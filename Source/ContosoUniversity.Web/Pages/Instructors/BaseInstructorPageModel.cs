using ContosoUniversity.Web.Data;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Web.Pages.Instructors;

public abstract class BaseInstructorPageModel : PageModel
{
    public IList<AssignedCourseData> AssignedCourses { get; set; } = new List<AssignedCourseData>();

    public void PopulateAssignedCourseData(SchoolDbContext context, Instructor instructor)
    {
        var allCourses = context.Courses;
        var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseId));

        foreach (var course in allCourses)
        {
            AssignedCourses.Add(new AssignedCourseData
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Assigned = instructorCourses.Contains(course.CourseId)
            });
        }
    }
}
