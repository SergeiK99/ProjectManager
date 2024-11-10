using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Models;
using ProjectManager.Models.ViewModels;

namespace ProjectManager.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["StartDateSortParm"] = sortOrder == "StartDate" ? "StartDate_desc" : "StartDate";
            ViewData["EndDateSortParm"] = sortOrder == "EndDate" ? "EndDate_desc" : "EndDate";
            ViewData["PrioritySortParm"] = sortOrder == "Priority" ? "Priority_desc" : "Priority";
            ViewData["CurrentFilter"] = searchString;

            var projects = from p in _context.Project.Include(p => p.ClientCompany)
                                                 .Include(p => p.ExecutorCompany)
                                                 .Include(p => p.ProjectManager)
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(p => p.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    projects = projects.OrderByDescending(p => p.Name);
                    break;
                case "StartDate":
                    projects = projects.OrderBy(p => p.StartDate);
                    break;
                case "StartDate_desc":
                    projects = projects.OrderByDescending(p => p.StartDate);
                    break;
                case "EndDate":
                    projects = projects.OrderBy(p => p.EndDate);
                    break;
                case "EndDate_desc":
                    projects = projects.OrderByDescending(p => p.EndDate);
                    break;
                case "Priority":
                    projects = projects.OrderBy(p => p.Priority);
                    break;
                case "Priority_desc":
                    projects = projects.OrderByDescending(p => p.Priority);
                    break;
                default:
                    projects = projects.OrderBy(p => p.Name);
                    break;
            }

            return View(await projects.ToListAsync());
        }

        // GET: Add Employee To Project
        public async Task<IActionResult> AddEmployeeToProject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var viewModel = new ProjectVM
            {
                Project = project,
                Employees = _context.Employee.ToList(),
                SelectedEmployeeIds = project.Employees.Select(pe => pe.EmployeeId).ToList()
            };

            return View(viewModel);
        }

        // POST: Add Employee To Project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployeeToProject(int id, ProjectVM projectVM)
        {
            if (ModelState.IsValid)
            {
                // Получаем проект из базы данных
                var project = await _context.Project.Include(p => p.Employees).FirstOrDefaultAsync(p => p.Id == id);

                if (project == null)
                {
                    return NotFound();
                }

                // Обновляем свойства проекта
                // ...

                // Обновляем список сотрудников проекта
                // Удаляем старые связи
                _context.ProjectEmployee.RemoveRange(project.Employees);
                // Добавляем новые связи
                foreach (var employeeId in projectVM.SelectedEmployeeIds)
                {
                    _context.ProjectEmployee.Add(new ProjectEmployee
                    {
                        ProjectId = project.Id,
                        EmployeeId = employeeId
                    });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectVM);
        }

        // GET: Projects details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.ClientCompany)
                .Include(p => p.ExecutorCompany)
                .Include(p => p.ProjectManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> ClientCompanyDropDown = _context.ClientCompany.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }
            );
            ViewBag.ClientCompanyDropDown = ClientCompanyDropDown;
            IEnumerable<SelectListItem> ExecutorCompanyDropDown = _context.ExecutorCompany.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }
            );
            ViewBag.ExecutorCompanyDropDown = ExecutorCompanyDropDown;
            IEnumerable<SelectListItem> EmployeeDropDown = _context.Employee.Select(i => new SelectListItem
            {
                Text = i.FirstName + " " + i.LastName,
                Value = i.Id.ToString()
            }
            );
            ViewBag.EmployeeDropDown = EmployeeDropDown;
            return View();
        }

        // POST: Projects create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> ClientCompanyDropDown = _context.ClientCompany.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }
            );
            ViewBag.ClientCompanyDropDown = ClientCompanyDropDown;
            IEnumerable<SelectListItem> ExecutorCompanyDropDown = _context.ExecutorCompany.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }
            );
            ViewBag.ExecutorCompanyDropDown = ExecutorCompanyDropDown;
            IEnumerable<SelectListItem> EmployeeDropDown = _context.Employee.Select(i => new SelectListItem
            {
                Text = i.FirstName + " " + i.LastName,
                Value = i.Id.ToString()
            }
            );
            ViewBag.EmployeeDropDown = EmployeeDropDown;
            return View(project);
        }

        // POST: Projects edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,Priority,ClientCompanyId,ExecutorCompanyId,ProjectManagerId")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.ClientCompany)
                .Include(p => p.ExecutorCompany)
                .Include(p => p.ProjectManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            if (project != null)
            {
                _context.Project.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
