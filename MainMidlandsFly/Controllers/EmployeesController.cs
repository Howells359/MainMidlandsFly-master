using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainMidlandsFly.Models;
using Microsoft.AspNetCore.Authorization;

namespace MainMidlandsFly.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly EmployeesContext _context;

        public EmployeesController(EmployeesContext context)
        {
            _context = context;
        }

// GET: Employees/Details/5
        public async Task<IActionResult> Index(string IDSEARCH)
        {
            var Employees = from a in _context.Employees
                         select a;
            if (!String.IsNullOrEmpty(IDSEARCH))
            {
                Employees = Employees.Where(a => a.EmployeesID.Contains(IDSEARCH));
            }

            return View(await Employees.ToListAsync());
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmployeesID,Name,DateOfBirth,Role")] Employees Employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Employees = await _context.Employees.SingleOrDefaultAsync(m => m.ID == id);
            if (Employees == null)
            {
                return NotFound();
            }
            return View(Employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EmployeesID,Name,DateOfBirth,Role")] Employees Employees)
        {
            if (id != Employees.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(Employees.ID))
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
            return View(Employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Employees = await _context.Employees
                .SingleOrDefaultAsync(m => m.ID == id);
            if (Employees == null)
            {
                return NotFound();
            }

            return View(Employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Employees = await _context.Employees.SingleOrDefaultAsync(m => m.ID == id);
            _context.Employees.Remove(Employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
