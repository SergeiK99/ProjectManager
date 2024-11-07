using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class ExecutorCompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExecutorCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExecutorCompany
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExecutorCompany.ToListAsync());
        }

        // GET: ExecutorCompany details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var executorCompany = await _context.ExecutorCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (executorCompany == null)
            {
                return NotFound();
            }

            return View(executorCompany);
        }

        // GET: ExecutorCompany create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExecutorCompany create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ExecutorCompany executorCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(executorCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(executorCompany);
        }

        // GET: ExecutorCompany edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var executorCompany = await _context.ExecutorCompany.FindAsync(id);
            if (executorCompany == null)
            {
                return NotFound();
            }
            return View(executorCompany);
        }

        // POST: ExecutorCompany edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ExecutorCompany executorCompany)
        {
            if (id != executorCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(executorCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExecutorCompanyExists(executorCompany.Id))
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
            return View(executorCompany);
        }

        // GET: ExecutorCompany delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var executorCompany = await _context.ExecutorCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (executorCompany == null)
            {
                return NotFound();
            }

            return View(executorCompany);
        }

        // POST: ExecutorCompany delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var executorCompany = await _context.ExecutorCompany.FindAsync(id);
            if (executorCompany != null)
            {
                _context.ExecutorCompany.Remove(executorCompany);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExecutorCompanyExists(int id)
        {
            return _context.ExecutorCompany.Any(e => e.Id == id);
        }
    }
}
