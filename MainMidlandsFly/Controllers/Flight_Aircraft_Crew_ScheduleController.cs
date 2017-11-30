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
            return View(await _context.Passenger_Aircraft_Crew_Schedule.ToListAsync());
        }

        // GET: Flight_Aircraft_Crew_Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight_Aircraft_Crew_Schedule = await _context.Passenger_Aircraft_Crew_Schedule
                .SingleOrDefaultAsync(m => m.ScheduleId == id);
            if (flight_Aircraft_Crew_Schedule == null)
            {
                return NotFound();
            }

            return View(flight_Aircraft_Crew_Schedule);
        }

        // GET: Flight_Aircraft_Crew_Schedule/Create
        public IActionResult CreateCargo(string id)
        {
            Cargo_Aircraft_Crew_Schedule_Model model = new Cargo_Aircraft_Crew_Schedule_Model();

            List<Aircraft> aircraft = new List<Aircraft>();

            aircraft = (from Aircraft in _context.Aircraft
                        select Aircraft).Where(a => a.Type == "Cargo" && a.Status == "Available").ToList();
            model.Aircraft_list = aircraft;

            List<Crew> cabin_crew = new List<Crew>();

            cabin_crew = (from Crew in _context.Crew
                          select Crew).Where(c => c.Type == "Cabin" && c.Status == "Available").ToList();

           

            model.CabinCrewId_list = cabin_crew;

            model.CabinCrewId2_list = cabin_crew;

            model.CabinCrewId3_list = cabin_crew;

         
            model.FlightId = Int32.Parse(id);

            return View(model);
          
        }
        public IActionResult CreatePassenger(string id)
        {
           Passenger_Flight_Crew_Schedule_Model model = new Passenger_Flight_Crew_Schedule_Model();

            List<Aircraft> aircraft = new List<Aircraft>();

            aircraft = (from Aircraft in _context.Aircraft 
                    select Aircraft).Where(a => a.Type == "Passenger" && a.Status == "Available").ToList();
            model.Aircraft_list = aircraft;

            List<Crew> cabin_crew = new List<Crew>();

            cabin_crew = (from Crew in _context.Crew
                        select Crew).Where(c => c.Type == "Cabin" && c.Status=="Available").ToList();

            List<Crew> flight_crew = new List<Crew>();

            flight_crew = (from Crew in _context.Crew
                          select Crew).Where(c => c.Type == "Flight" && c.Status == "Available").ToList();

            model.CabinCrewId_list = cabin_crew;

            model.CabinCrewId2_list = cabin_crew;

            model.FlightCrewId1_list = flight_crew;

            model.FlightCrewId2_list = flight_crew;

            model.FlightCrewId3_list = flight_crew;
            model.FlightId = Int32.Parse(id);

            return View(model);
        }

        // POST: Flight_Aircraft_Crew_Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertPassengerSchedule([Bind("FlightId,AircraftId,CabinCrewId,CabinCrewId2,FlightCrewId1,FlightCrewId2,FlightCrewId3,Flying_Hours")] Passenger_Flight_Crew_Schedule_Model schedule)
        {
            if (ModelState.IsValid)
            {
                Passenger_Aircraft_Crew_Schedule m = new Passenger_Aircraft_Crew_Schedule();
                m.FlightId = schedule.FlightId;
                m.AircraftId = schedule.AircraftId;
                m.CabinCrewId = schedule.CabinCrewId;
                m.CabinCrewId2 = schedule.CabinCrewId2;
                m.FlightCrewId1 = schedule.FlightCrewId1;
                m.FlightCrewId2 = schedule.FlightCrewId2;
                m.FlightCrewId3 = schedule.FlightCrewId3;


                _context.Add(m);
                await _context.SaveChangesAsync();

                //  return RedirectToAction("CreateCargo", "Flight_Aircraft_Crew_Schedule", new { id = m.FlightId.ToString()});

                return RedirectToAction("PlaneChoice", "Flight");
               // return RedirectToAction(nameof(PlaneChoice));

            }
            return View(schedule);
        }


        public async Task<IActionResult> InsertCargoSchedule([Bind("FlightId,AircraftId,CabinCrewId,CabinCrewId2,CabinCrewId3,Flying_Hours")] Cargo_Aircraft_Crew_Schedule_Model schedule)
        {
            if (ModelState.IsValid)
            {
                Cargo_Aircraft_Crew_Schedule m = new Cargo_Aircraft_Crew_Schedule();
                m.FlightId = schedule.FlightId;
                m.AircraftId = schedule.AircraftId;
                m.CabinCrewId = schedule.CabinCrewId;
                m.CabinCrewId2 = schedule.CabinCrewId2;
                m.CabinCrewId3 = schedule.CabinCrewId3;


                _context.Add(m);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Flight_Aircraft_Crew_Schedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight_Aircraft_Crew_Schedule = await _context.Passenger_Aircraft_Crew_Schedule.SingleOrDefaultAsync(m => m.ScheduleId == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,FlightId,AircraftId,CabinCrewId,CabinCrewId2,CabinCrewId3,FlightCrewId1,FlightCrewId2,FlightCrewId3,Flying_Hours")] Passenger_Aircraft_Crew_Schedule flight_Aircraft_Crew_Schedule)
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

            var flight_Aircraft_Crew_Schedule = await _context.Passenger_Aircraft_Crew_Schedule
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
            var flight_Aircraft_Crew_Schedule = await _context.Passenger_Aircraft_Crew_Schedule.SingleOrDefaultAsync(m => m.ScheduleId == id);
            _context.Passenger_Aircraft_Crew_Schedule.Remove(flight_Aircraft_Crew_Schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Flight_Aircraft_Crew_ScheduleExists(int id)
        {
            return _context.Passenger_Aircraft_Crew_Schedule.Any(e => e.ScheduleId == id);
        }
    }
}
