using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainMidlandsFly.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace MainMidlandsFly.Controllers
{
    //[Authorize(Roles = "Admin, Crew")]
    public class FlightController : Controller
    {
        private readonly MainFlightContext _context;

        public FlightController(MainFlightContext context)
        {
            _context = context;
        }


        // GET: Flight
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flight.ToListAsync());
        }


        // GET: Flight/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .SingleOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }


        // GET: Flight/Create
        public IActionResult Create()
        {
            var flight = new Flight();

            return View(flight);
        }

        
        // POST: Flight/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,FlightType,LeavingDate, ArrivalDate, Origin,Destination,")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Flight_Aircraft_Crew_ScheduleController.Create));
            }
            if (flight.FlightType == "Passenger")
            {
                TempData["flightData"] = JsonConvert.SerializeObject(flight);

                //return RedirectToAction("CreateForPassenger", flight);
                return RedirectToAction("CreateForPassenger", "Flight"); 
            }

            if (flight.FlightType == "Cargo")
            {
                //persist data for next request - .NET Core doesn't support adding model objects to TempData, 
                //so need to either store the individual properties or serialize/deserialze via a JSON or XML string
                TempData["flightData"] = JsonConvert.SerializeObject(flight);
                
                //Redirect to Action not View so Get method is performed to query and list available aircraft
                return RedirectToAction("CreateForCargo", "Flight");
                
            }
            return View();
        }






        //GET: CreateForCargo
        [HttpGet]
        //public IActionResult CreateForCargo(int id)
        public IActionResult CreateForCargo()
        {

            //Deserialise TempData in Flight object
            Flight flight = JsonConvert.DeserializeObject<Flight>(TempData["FlightData"].ToString());


            //Check aircraft availability and create list of them for drop down View           
            List<Aircraft> aircraft = new List<Aircraft>();

            //Select Aircraft 
            aircraft = (from Aircraft in _context.Aircraft where Aircraft.Type == "Cargo"
                                                           select Aircraft
                                                           ).ToList();

            List<Flight> assignedAircraft = new List<Flight>();
            assignedAircraft = (from Flight in _context.Flight select Flight).ToList();

            List<string> listString = (from e in aircraft where !(from m in assignedAircraft
                                                                      //=> n.Date >= firstdate.Date)
                                                                  where m.LeavingDate <= flight.LeavingDate
                                                                  where m.ArrivalDate >= flight.LeavingDate
                                                                  select m.AircraftRegNo).Contains(e.AircraftRegNo)
                                                                  select e.AircraftRegNo).ToList();

            //listString.Insert(0, new Flight { FlightId = 0, AircraftRegNo = "" });
            ViewBag.ListOfAircraft = listString;
            
            //return RedirectToAction("Index");
            return View(flight);
        }



        //POST CreateForCargo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForCargo(int FlightId,[Bind("FlightId,FlightType,AircraftRegNo,LeavingDate,DepartureTime,ArrivalDate,ArrivalTime,Origin,Destination")] Flight flight)       
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {               
                _context.Update(flight);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }





            //Get CreateForPassenger
            public IActionResult CreateForPassenger()
        {
            //Deserialise TempData in Flight object
            Flight flight = JsonConvert.DeserializeObject<Flight>(TempData["FlightData"].ToString());


            //Check aircraft availability and create list of them for drop down View           
            List<Aircraft> aircraft = new List<Aircraft>();

            //Select Aircraft 
            aircraft = (from Aircraft in _context.Aircraft
                        where Aircraft.Type == "Passenger"
                        select Aircraft
                        ).ToList();

            List<Flight> assignedAircraft = new List<Flight>();
            assignedAircraft = (from Flight in _context.Flight select Flight).ToList();

            List<string> listString = (from e in aircraft
                                       where !(from m in assignedAircraft                                                  
                                               where m.LeavingDate <= flight.LeavingDate
                                               where m.ArrivalDate >= flight.LeavingDate
                                               select m.AircraftRegNo).Contains(e.AircraftRegNo)
                                       select e.AircraftRegNo).ToList();

            //listString.Insert(0, new Flight { FlightId = 0, AircraftRegNo = "" });
            ViewBag.ListOfAircraft = listString;

            //return RedirectToAction("Index");
            return View(flight);
        }

        //POST: CreateForPassenger
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForPassenger(int FlightId, [Bind("FlightId,FlightType,AircraftRegNo,LeavingDate,DepartureTime,ArrivalDate,ArrivalTime,Origin,Destination")] Flight flight)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _context.Update(flight);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }



        // GET: Flight/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.SingleOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: Flight/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,LeavingDate,DepartureTime,ArrivalDate,ArrivalTime,Origin,Destination")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightId))
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
            return View(flight);
        }

        // GET: Flight/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .SingleOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flight.SingleOrDefaultAsync(m => m.FlightId == id);
            _context.Flight.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flight.Any(e => e.FlightId == id);
        }
    }
}
