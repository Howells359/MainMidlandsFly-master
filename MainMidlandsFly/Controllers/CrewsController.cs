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
    [Authorize(Roles = "Admin, Crew")]
    public class CrewsController : Controller
    {
        private readonly NewCrewContext _context;

        public CrewsController(NewCrewContext context)
        {
            _context = context;
        }

        // GET: Crews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crew.ToListAsync());
        }

        // GET: Crews/Details/5
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

        // GET: Crews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CrewId,Name,MobNo,Email,Address,DateOfBirth,Type,Status")] Crew crew)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crew);
        }

        // GET: Crews/Edit/5
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

        // POST: Crews/Edit/5
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

        // GET: Crews/Delete/5
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

        // POST: Crews/Delete/5
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
