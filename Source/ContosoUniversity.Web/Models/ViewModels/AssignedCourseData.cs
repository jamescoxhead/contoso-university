namespace ContosoUniversity.Web.Models.ViewModels;

public class AssignedCourseData
{
    public required int CourseId { get; set; }
    public required string Title { get; set; }
    public bool Assigned { get; set; }
}
