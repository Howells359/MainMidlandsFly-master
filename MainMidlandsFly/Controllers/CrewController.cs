using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainMidlandsFly.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;


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
            ////create Address object to store form values, may not need to create the object here??
            var addressIndex = new Crew();
            ////Message string to be used by Index View
            string msg = "Please input house number/name and post code.";
            ViewBag.msg = msg;
            ////again not sure if addressIndex required here as object is empty??
            return View(addressIndex);
            //return View();
        }



        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string postcode, string HouseNo, string Name, string Type, string Email, string MobNo, DateTime DateOfBirth)
            {
                //Use plugin Net.Http to connect to API, create new object
                using (HttpClient client = new HttpClient())
                {
                    //Create new object to store form values in Address model
                    var FullAddress = new Crew();

                    //Store any populated form values in FullAddress Address model object
                    if (Name != null) { FullAddress.Name = Name; }
                    if (Type != null) { FullAddress.Type = Type; }
                    if (Email != null) { FullAddress.Email = Email; }
                    if (MobNo != null) { FullAddress.MobNo = MobNo; }
                    if (DateOfBirth != null) { FullAddress.MobNo = MobNo; }
                


                //Connection details for GetAddress.io API that returns full addresses when House No/Name & Postcode provided
                string apiKey = "xmZfShMoG0aWHJRPc-FkSQ11213";
                    string website = "https://api.getaddress.io/find/" + postcode + "/" + HouseNo + "?api-key=" + apiKey;


                    client.BaseAddress = new Uri(website);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.GetAsync("").Result;

                    //If API successfully returns address run following if
                    if (response.IsSuccessStatusCode)
                    {
                        //create object with API JSON response value
                        var AddressResponse = response.Content.ReadAsStringAsync().Result;

                        //Use Newtonsoft JSON plugin to deserialize response
                        var address1 = JsonConvert.DeserializeObject<Crew>(AddressResponse);

                        //string AddressLatitude = address.latitude;

                        //GetCrew() API always returns address values as an array with each address line being comma spearated values.
                        //As we're only looking for singular addresses we always use the first and only value in the return array and assign it to a string.
                        string fullAddress = address1.addresses[0];

                        //Separate address string into an array using comma separator to identify values, removes any white space.
                        string[] values = fullAddress.Split(',').Select(sValue => sValue.Trim()).ToArray();

                        //Assign array values to associated strings
                        string AddressLine1 = values[0];
                        string AddressLine2 = values[1];
                        string AddressLine3 = values[2];
                        string AddressLine4 = values[3];
                        string Locality = values[4];
                        string TownOrCity = values[5];
                        string County = values[6];

                        //create array of the strings
                        string[] myStrings = new string[] { AddressLine1, AddressLine2, AddressLine3, AddressLine4, Locality, TownOrCity, County, postcode, null };

                        //Build full address string from populated (not null) values adding line break between each line 
                        address1.formattedAddress = string.Join(System.Environment.NewLine, myStrings.Where(str => !string.IsNullOrEmpty(str)));


                        if (address1.formattedAddress != null)
                        {
                        //store address value in formattedAddress model object
                        //FullAddress.formattedAddress = address1.formattedAddress;
                        FullAddress.Address = address1.formattedAddress;
                        return View(FullAddress);
                        }
                    }
                    //If address not found send following message to View
                    string msg = "Address not found, please try again.";
                    ViewBag.msg = msg;
                    return View(FullAddress);
                }
            }


        // POST: Crew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdateDB([Bind("CrewId,Name,MobNo,Email,Address,DateOfBirth,Type,Status")] Crew crew)
        {
            //Code below writes form values to SQL DB
            if (ModelState.IsValid)
            {
                _context.Add(crew);
                await _context.SaveChangesAsync();
                //To create unique 5 digit employee ID utilise CrewID primary key when new employee created and left pad value with zeros 
                //string EmployeeID = (crew.CrewId).ToString().PadLeft(5, '0');
                _context.Update(crew);
                return RedirectToAction(nameof(Index));
            }
            //return View(crew);
            return RedirectToAction("Create", crew);
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
