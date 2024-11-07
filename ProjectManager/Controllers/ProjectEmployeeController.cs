using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class ProjectEmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectEmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Add Employee To Project
        public async Task<IActionResult> AddEmployeeToProject(int ProjectId)
        {
            var project = await _context.Project.FindAsync(ProjectId);
            if (project == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> EmployeeDropDown = _context.Employee.Select(i => new SelectListItem
            {
                Text = i.FirstName + " " + i.LastName,
                Value = i.Id.ToString()
            });
            ViewBag.EmployeeDropDown = EmployeeDropDown;
            return View(project);
        }

        // POST: Add Employee To Project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployeeToProject(int ProjectId, int EmployeeId)
        {
            var projectEmployee = new ProjectEmployee { ProjectId = ProjectId, EmployeeId = EmployeeId };

            _context.Add(projectEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }

        // POST: Remove Employee From Project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveEmployeeFromProject(int ProjectId, int EmployeeId)
        {
            var projectEmployee = await _context.ProjectEmployee.FirstOrDefaultAsync(pe => pe.ProjectId == ProjectId && pe.EmployeeId == EmployeeId);
            if (projectEmployee != null)
            {
                _context.ProjectEmployee.Remove(projectEmployee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Projects");
        }
    }
}
