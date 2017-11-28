using MainMidlandsFly.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly.Controllers
{
    //[Authorize(Roles = "Admin, Crew")]
    public class AircraftController : Controller
    {
        private readonly NewAircraftContext _context;

        public AircraftController(NewAircraftContext context)
        {
            _context = context;
        }

        // GET: Aircraft
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aircraft.ToListAsync());
        }

        // GET: Aircraft/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft
                .SingleOrDefaultAsync(m => m.ID == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // GET: Aircraft/Create
        public IActionResult Create()
        {
            //Model object required to populate 'Type' drop down
            Aircraft aircraft = new Aircraft();
            return View(aircraft);
        }


        //Validation: Called by Aircraft model when providing input to AircraftRegNo field to check value is unique in DB
        public IActionResult RegIdValidator(string AircraftRegNo, string InitialRegId)
        {
            //Line below is required when editing an exisitng record as AircraftRegNo model validation fails 
            //as existing record is idenitified as a duplicate value
            bool InitialRegIdExists = _context.Aircraft.Any(x => x.AircraftRegNo == InitialRegId);

            if (InitialRegIdExists)
            {
                return Json(true);
            }

            //When Aircraft record is created the section below ensure AircraftRegNo is unique in the DB
            bool RegIdExists = _context.Aircraft.Any(x => x.AircraftRegNo == AircraftRegNo);
            if  (RegIdExists)
            {                
                return Json(data: $"Aircraft registration number {AircraftRegNo} already exists.");
            }
            return Json(true);            
        }


        // POST: Aircraft/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AircraftRegNo,Type,FlyingHoursCount,Maximum_Seating_Capacity,Maximum_Cargo_Capacity")] Aircraft aircraft)
        {
            //Write input values to DB
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _context.Add(aircraft);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        // GET: Aircraft/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft.SingleOrDefaultAsync(m => m.ID == id);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

        // POST: Aircraft/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, AircraftRegNo, MaxCarry,MaxSeat,Type,Status,FlyingHoursCount,Maximum_Seating_Capacity,Maximum_Cargo_Capacity")] Aircraft aircraft)
        {
            if (id != aircraft.ID)
            {
                return NotFound();
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            //else
            {
                try
                {
                    _context.Update(aircraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftExists(aircraft.ID))
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
            return View(aircraft);
        }

        // GET: Aircraft/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft
                .SingleOrDefaultAsync(m => m.ID == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aircraft = await _context.Aircraft.SingleOrDefaultAsync(m => m.ID == id);
            _context.Aircraft.Remove(aircraft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftExists(int id)
        {
            return _context.Aircraft.Any(e => e.ID == id);
        }
    }
}
