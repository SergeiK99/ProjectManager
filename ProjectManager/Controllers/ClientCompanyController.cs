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
    public class ClientCompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientCompany
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientCompany.ToListAsync());
        }

        // GET: ClientCompany details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientCompany = await _context.ClientCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientCompany == null)
            {
                return NotFound();
            }

            return View(clientCompany);
        }

        // GET: ClientCompany create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientCompany create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ClientCompany clientCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientCompany);
        }

        // GET: ClientCompany edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientCompany = await _context.ClientCompany.FindAsync(id);
            if (clientCompany == null)
            {
                return NotFound();
            }
            return View(clientCompany);
        }

        // POST: ClientCompany edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ClientCompany clientCompany)
        {
            if (id != clientCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientCompanyExists(clientCompany.Id))
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
            return View(clientCompany);
        }

        // GET: ClientCompany delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientCompany = await _context.ClientCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientCompany == null)
            {
                return NotFound();
            }

            return View(clientCompany);
        }

        // POST: ClientCompany delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientCompany = await _context.ClientCompany.FindAsync(id);
            if (clientCompany != null)
            {
                _context.ClientCompany.Remove(clientCompany);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientCompanyExists(int id)
        {
            return _context.ClientCompany.Any(e => e.Id == id);
        }
    }
}
