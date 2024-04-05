using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Models.Entities;
using ContosoUniversity.Web.Data;

namespace ContosoUniversity.Web.Pages.Departments;

public class EditModel(SchoolDbContext context) : PageModel
{
    private readonly SchoolDbContext _context = context;

    [BindProperty]
    public Department Department { get; set; } = default!;

    public SelectList InstructorNames { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var department = await _context.Departments.Include(d => d.Administrator)
                                                   .AsNoTracking()
                                                   .FirstOrDefaultAsync(m => m.DepartmentId == id);
        if (department == null)
        {
            return NotFound();
        }

        Department = department;
        InstructorNames = new SelectList(_context.Instructors, "InstructorId", "FirstMidName");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updateDepartment = await _context.Departments.Include(i => i.Administrator).FirstOrDefaultAsync(dept => dept.DepartmentId == id);

        if (updateDepartment == null)
        {
            return HandleDeletedDepartment();
        }

        _context.Entry(updateDepartment).Property(d => d.RowVersion).OriginalValue = Department.RowVersion;

        if (await TryUpdateModelAsync(updateDepartment, "Department", s => s.Name, s => s.StartDate, s => s.Budget, s => s.InstructorId))
        {
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientValues = (Department)exceptionEntry.Entity;
                var databaseEntry = exceptionEntry.GetDatabaseValues();

                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save. The department was deleted by another user.");
                    return Page();
                }

                var dbValues = (Department)databaseEntry.ToObject();
                await SetDbErrorMessage(dbValues, clientValues);

                // Save the current ConcurrencyToken so next postback
                // matches unless an new concurrency issue happens.
                Department.RowVersion = dbValues.RowVersion;
                // Clear the model error for the next postback.
                ModelState.Remove($"{nameof(Department)}.{nameof(Department.RowVersion)}");
            }
        }

        _context.Attach(Department).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(Department.DepartmentId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        InstructorNames = new SelectList(_context.Instructors, "InstructorId", "FirstMidName");

        return Page();
    }

    private bool DepartmentExists(int id) => _context.Departments.Any(e => e.DepartmentId == id);

    private PageResult HandleDeletedDepartment()
    {
        ModelState.AddModelError(string.Empty, "Unable to save. The department was deleted by another user.");
        InstructorNames = new SelectList(_context.Instructors, "InstructorId", "FirstMidName");

        return Page();
    }

    private async Task SetDbErrorMessage(Department dbValues, Department clientValues)
    {
        if (dbValues.Name != clientValues.Name)
        {
            ModelState.AddModelError("Department.Name", $"Current value: {dbValues.Name}");
        }

        if (dbValues.Budget != clientValues.Budget)
        {
            ModelState.AddModelError("Department.Budget", $"Current value: {dbValues.Budget:c}");
        }

        if (dbValues.StartDate != clientValues.StartDate)
        {
            ModelState.AddModelError("Department.StartDate", $"Current value: {dbValues.StartDate:d}");
        }

        if (dbValues.InstructorId != clientValues.InstructorId)
        {
            var dbInstructor = await _context.Instructors.FindAsync(dbValues.InstructorId);
            ModelState.AddModelError("Department.InstructorId", $"Current value: {dbInstructor?.FullName}");
        }

        ModelState.AddModelError(string.Empty,
            "The record you attempted to edit "
          + "was modified by another user after you. The "
          + "edit operation was canceled and the current values in the database "
          + "have been displayed. If you still want to edit this record, click "
          + "the Save button again.");
    }
}
