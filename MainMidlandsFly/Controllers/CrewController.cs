using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainMidlandsFly.Models;

namespace MainMidlandsFly.Controllers
{
    public class CrewController : Controller
    {
        private readonly CrewContext _context;

        public CrewController(CrewContext context)
        {
            _context = context;
        }

// GET: Crew/Details/5
        public async Task<IActionResult> Index(string IDSEARCH)
        {
            var Crew = from a in _context.Crew
                         select a;
            if (!String.IsNullOrEmpty(IDSEARCH))
            {
                Crew = Crew.Where(a => a.CrewID.Contains(IDSEARCH));
            }

            return View(await Crew.ToListAsync());
        }

        // GET: Crew/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CrewID,Name,DateOfBirth,Type")] Crew crew)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crew);
        }

        // GET: Crew/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crew = await _context.Crew.SingleOrDefaultAsync(m => m.ID == id);
            if (crew == null)
            {
                return NotFound();
            }
            return View(crew);
        }

        // POST: Crew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CrewID,Name,DateOfBirth,Type")] Crew crew)
        {
            if (id != crew.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrewExists(crew.ID))
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
            return View(crew);
        }

        // GET: Crew/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crew = await _context.Crew
                .SingleOrDefaultAsync(m => m.ID == id);
            if (crew == null)
            {
                return NotFound();
            }

            return View(crew);
        }

        // POST: Crew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crew = await _context.Crew.SingleOrDefaultAsync(m => m.ID == id);
            _context.Crew.Remove(crew);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrewExists(int id)
        {
            return _context.Crew.Any(e => e.ID == id);
        }
    }
}
