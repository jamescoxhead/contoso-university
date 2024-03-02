using System.Globalization;
using ContosoUniversity.Web.Models.Entities;

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

        // STUDENTS
        var alexander = new Student
        {
            FirstMidName = "Carson",
            LastName = "Alexander",
            EnrollmentDate = DateTime.Parse("2016-09-01", culture)
        };
        var alonso = new Student
        {
            FirstMidName = "Meredith",
            LastName = "Alonso",
            EnrollmentDate = DateTime.Parse("2018-09-01", culture)
        };
        var anand = new Student
        {
            FirstMidName = "Arturo",
            LastName = "Anand",
            EnrollmentDate = DateTime.Parse("2019-09-01", culture)
        };
        var barzdukas = new Student
        {
            FirstMidName = "Gytis",
            LastName = "Barzdukas",
            EnrollmentDate = DateTime.Parse("2018-09-01", culture)
        };
        var li = new Student
        {
            FirstMidName = "Yan",
            LastName = "Li",
            EnrollmentDate = DateTime.Parse("2018-09-01", culture)
        };
        var justice = new Student
        {
            FirstMidName = "Peggy",
            LastName = "Justice",
            EnrollmentDate = DateTime.Parse("2017-09-01", culture)
        };
        var norman = new Student
        {
            FirstMidName = "Laura",
            LastName = "Norman",
            EnrollmentDate = DateTime.Parse("2019-09-01", culture)
        };
        var olivetto = new Student
        {
            FirstMidName = "Nino",
            LastName = "Olivetto",
            EnrollmentDate = DateTime.Parse("2011-09-01", culture)
        };

        var students = new Student[]
        {
            alexander,
            alonso,
            anand,
            barzdukas,
            li,
            justice,
            norman,
            olivetto
        };

        context.Students.AddRange(students);

        // INSTRUCTORS
        var abercrombie = new Instructor
        {
            FirstMidName = "Kim",
            LastName = "Abercrombie",
            HireDate = DateTime.Parse("1995-03-11", culture)
        };
        var fakhouri = new Instructor
        {
            FirstMidName = "Fadi",
            LastName = "Fakhouri",
            HireDate = DateTime.Parse("2002-07-06", culture)
        };
        var harui = new Instructor
        {
            FirstMidName = "Roger",
            LastName = "Harui",
            HireDate = DateTime.Parse("1998-07-01", culture)
        };
        var kapoor = new Instructor
        {
            FirstMidName = "Candace",
            LastName = "Kapoor",
            HireDate = DateTime.Parse("2001-01-15", culture)
        };
        var zheng = new Instructor
        {
            FirstMidName = "Roger",
            LastName = "Zheng",
            HireDate = DateTime.Parse("2004-02-12", culture)
        };
        var instructors = new Instructor[]
        {
            abercrombie,
            fakhouri,
            harui,
            kapoor,
            zheng
        };

        context.Instructors.AddRange(instructors);

        // OFFICE ASSIGNMENTS
        var officeAssignments = new OfficeAssignment[]
        {
            new() {
                Instructor = fakhouri,
                Location = "Smith 17" },
            new() {
                Instructor = harui,
                Location = "Gowan 27" },
            new() {
                Instructor = kapoor,
                Location = "Thompson 304" }
        };

        context.OfficeAssignments.AddRange(officeAssignments);

        // DEPARTMENTS
        var english = new Department
        {
            Name = "English",
            Budget = 350000,
            StartDate = DateTime.Parse("2007-09-01", culture),
            Administrator = abercrombie
        };
        var mathematics = new Department
        {
            Name = "Mathematics",
            Budget = 100000,
            StartDate = DateTime.Parse("2007-09-01", culture),
            Administrator = fakhouri
        };
        var engineering = new Department
        {
            Name = "Engineering",
            Budget = 350000,
            StartDate = DateTime.Parse("2007-09-01", culture),
            Administrator = harui
        };
        var economics = new Department
        {
            Name = "Economics",
            Budget = 100000,
            StartDate = DateTime.Parse("2007-09-01", culture),
            Administrator = kapoor
        };

        var departments = new Department[]
        {
            english,
            mathematics,
            engineering,
            economics
        };

        context.Departments.AddRange(departments);

        // COURSES
        var chemistry = new Course
        {
            CourseId = 1050,
            Title = "Chemistry",
            Credits = 3,
            Department = engineering,
            Instructors = new List<Instructor> { kapoor, harui }
        };
        var microeconomics = new Course
        {
            CourseId = 4022,
            Title = "Microeconomics",
            Credits = 3,
            Department = economics,
            Instructors = new List<Instructor> { zheng }
        };
        var macroeconmics = new Course
        {
            CourseId = 4041,
            Title = "Macroeconomics",
            Credits = 3,
            Department = economics,
            Instructors = new List<Instructor> { zheng }
        };
        var calculus = new Course
        {
            CourseId = 1045,
            Title = "Calculus",
            Credits = 4,
            Department = mathematics,
            Instructors = new List<Instructor> { fakhouri }
        };
        var trigonometry = new Course
        {
            CourseId = 3141,
            Title = "Trigonometry",
            Credits = 4,
            Department = mathematics,
            Instructors = new List<Instructor> { harui }
        };
        var composition = new Course
        {
            CourseId = 2021,
            Title = "Composition",
            Credits = 3,
            Department = english,
            Instructors = new List<Instructor> { abercrombie }
        };
        var literature = new Course
        {
            CourseId = 2042,
            Title = "Literature",
            Credits = 4,
            Department = english,
            Instructors = new List<Instructor> { abercrombie }
        };

        var courses = new Course[]
        {
            chemistry,
            microeconomics,
            macroeconmics,
            calculus,
            trigonometry,
            composition,
            literature
        };

        context.Courses.AddRange(courses);

        var enrollments = new Enrollment[]
        {
            new() {
                Student = alexander,
                Course = chemistry,
                Grade = Grade.A
            },
            new() {
                Student = alexander,
                Course = microeconomics,
                Grade = Grade.C
            },
            new() {
                Student = alexander,
                Course = macroeconmics,
                Grade = Grade.B
            },
            new() {
                Student = alonso,
                Course = calculus,
                Grade = Grade.B
            },
            new() {
                Student = alonso,
                Course = trigonometry,
                Grade = Grade.B
            },
            new() {
                Student = alonso,
                Course = composition,
                Grade = Grade.B
            },
            new() {
                Student = anand,
                Course = chemistry
            },
            new() {
                Student = anand,
                Course = microeconomics,
                Grade = Grade.B
            },
            new() {
                Student = barzdukas,
                Course = chemistry,
                Grade = Grade.B
            },
            new() {
                Student = li,
                Course = composition,
                Grade = Grade.B
            },
            new() {
                Student = justice,
                Course = literature,
                Grade = Grade.B
            }
        };

        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
    }
}
