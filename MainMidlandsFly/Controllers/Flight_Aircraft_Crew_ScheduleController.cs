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
    //[Authorize(Roles = "Admin, Crew")]
    public class Flight_Aircraft_Crew_ScheduleController : Controller
    {
        private readonly MainFlightContext _context;

        public Flight_Aircraft_Crew_ScheduleController(MainFlightContext context)
        {
            _context = context;
        }

        // GET: Flight_Aircraft_Crew_Schedule
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flight_Aircraft_Crew_Schedule.ToListAsync());
        }

        // GET: Flight_Aircraft_Crew_Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight_Aircraft_Crew_Schedule = await _context.Flight_Aircraft_Crew_Schedule
                .SingleOrDefaultAsync(m => m.ScheduleId == id);
            if (flight_Aircraft_Crew_Schedule == null)
            {
                return NotFound();
            }

            return View(flight_Aircraft_Crew_Schedule);
        }

        // GET: Flight_Aircraft_Crew_Schedule/Create
        public IActionResult CreateCargo()
        {
            return View();
        }
        public IActionResult CreatePassenger()
        {
            return View();
        }

        // POST: Flight_Aircraft_Crew_Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,FlightId,AircraftId,CabinCrewId,CabinCrewId2,CabinCrewId3,FlightCrewId1,FlightCrewId2,FlightCrewId3,Flying_Hours")] Flight_Aircraft_Crew_Schedule flight_Aircraft_Crew_Schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight_Aircraft_Crew_Schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flight_Aircraft_Crew_Schedule);
        }
     
        // GET: Flight_Aircraft_Crew_Schedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight_Aircraft_Crew_Schedule = await _context.Flight_Aircraft_Crew_Schedule.SingleOrDefaultAsync(m => m.ScheduleId == id);
            if (flight_Aircraft_Crew_Schedule == null)
            {
                return NotFound();
            }
            return View(flight_Aircraft_Crew_Schedule);
        }

        // POST: Flight_Aircraft_Crew_Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,FlightId,AircraftId,CabinCrewId,CabinCrewId2,CabinCrewId3,FlightCrewId1,FlightCrewId2,FlightCrewId3,Flying_Hours")] Flight_Aircraft_Crew_Schedule flight_Aircraft_Crew_Schedule)
        {
            if (id != flight_Aircraft_Crew_Schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight_Aircraft_Crew_Schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Flight_Aircraft_Crew_ScheduleExists(flight_Aircraft_Crew_Schedule.ScheduleId))
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
            return View(flight_Aircraft_Crew_Schedule);
        }

        // GET: Flight_Aircraft_Crew_Schedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight_Aircraft_Crew_Schedule = await _context.Flight_Aircraft_Crew_Schedule
                .SingleOrDefaultAsync(m => m.ScheduleId == id);
            if (flight_Aircraft_Crew_Schedule == null)
            {
                return NotFound();
            }

            return View(flight_Aircraft_Crew_Schedule);
        }

        // POST: Flight_Aircraft_Crew_Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight_Aircraft_Crew_Schedule = await _context.Flight_Aircraft_Crew_Schedule.SingleOrDefaultAsync(m => m.ScheduleId == id);
            _context.Flight_Aircraft_Crew_Schedule.Remove(flight_Aircraft_Crew_Schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Flight_Aircraft_Crew_ScheduleExists(int id)
        {
            return _context.Flight_Aircraft_Crew_Schedule.Any(e => e.ScheduleId == id);
        }
    }
}
