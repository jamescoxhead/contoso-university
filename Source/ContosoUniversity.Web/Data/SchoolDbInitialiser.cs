using System.Globalization;
using ContosoUniversity.Web.Models;

namespace ContosoUniversity.Web.Data;

public class SchoolDbInitialiser
{
    public static void Initialise(SchoolDbContext context)
    {
        var culture = CultureInfo.CreateSpecificCulture("en-GB");

        // Look for any students.
        if (context.Students.Any())
        {
            return;   // DB has been seeded
        }

        var students = new Student[]
        {
            new() {FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2019-09-01", culture)},
            new() {FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2017-09-01", culture)},
            new() {FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2018-09-01", culture)},
            new() {FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2017-09-01", culture)},
            new() {FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2017-09-01", culture)},
            new() {FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2016-09-01", culture)},
            new() {FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2018-09-01", culture)},
            new() {FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2019-09-01", culture)}
        };

        context.Students.AddRange(students);
        context.SaveChanges();

        var courses = new Course[]
        {
            new() {CourseId=1050,Title="Chemistry",Credits=3},
            new() {CourseId=4022,Title="Microeconomics",Credits=3},
            new() {CourseId=4041,Title="Macroeconomics",Credits=3},
            new() {CourseId=1045,Title="Calculus",Credits=4},
            new() {CourseId=3141,Title="Trigonometry",Credits=4},
            new() {CourseId=2021,Title="Composition",Credits=3},
            new() {CourseId=2042,Title="Literature",Credits=4}
        };

        context.Courses.AddRange(courses);
        context.SaveChanges();

        var enrollments = new Enrollment[]
        {
            new() {StudentId=1,CourseId=1050,Grade=Grade.A},
            new() {StudentId=1,CourseId=4022,Grade=Grade.C},
            new() {StudentId=1,CourseId=4041,Grade=Grade.B},
            new() {StudentId=2,CourseId=1045,Grade=Grade.B},
            new() {StudentId=2,CourseId=3141,Grade=Grade.F},
            new() {StudentId=2,CourseId=2021,Grade=Grade.F},
            new() {StudentId=3,CourseId=1050},
            new() {StudentId=4,CourseId=1050},
            new() {StudentId=4,CourseId=4022,Grade=Grade.F},
            new() {StudentId=5,CourseId=4041,Grade=Grade.C},
            new() {StudentId=6,CourseId=1045},
            new() {StudentId=7,CourseId=3141,Grade=Grade.A},
        };

        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
    }
}
