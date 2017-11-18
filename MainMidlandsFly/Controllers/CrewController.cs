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

        // GET: Crew
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crew.ToListAsync());
        }

        // GET: Crew/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crew = await _context.Crew
                .SingleOrDefaultAsync(m => m.CrewId == id);
            if (crew == null)
            {
                return NotFound();
            }

            return View(crew);
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
        public async Task<IActionResult> Create([Bind("CrewId,Name,MobNo,Email,Address,DateOfBirth,Type,Status")] Crew crew)
        {
#
            if (ModelState.IsValid)
            {
                _context.Add(crew);
                await _context.SaveChangesAsync();
                //To create unique 5 digit employee ID utilise CrewID primary key when new employee created and left pad value with zeros 
                //string EmployeeID = (crew.CrewId).ToString().PadLeft(5, '0');
                _context.Update(crew.)
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

            var crew = await _context.Crew.SingleOrDefaultAsync(m => m.CrewId == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("CrewId,Name,MobNo,Email,Address,DateOfBirth,Type,Status")] Crew crew)
        {
            if (id != crew.CrewId)
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
                    if (!CrewExists(crew.CrewId))
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
                .SingleOrDefaultAsync(m => m.CrewId == id);
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
            var crew = await _context.Crew.SingleOrDefaultAsync(m => m.CrewId == id);
            _context.Crew.Remove(crew);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrewExists(int id)
        {
            return _context.Crew.Any(e => e.CrewId == id);
        }
    }
}
