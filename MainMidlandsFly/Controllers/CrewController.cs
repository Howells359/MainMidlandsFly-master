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
            //create Crew object to provide data for dropdown list form values
            var crewCreate = new CrewViewModel();
            
            //Message string to be used by Index View
            string msg = "Please input house number/name and post code.";
            ViewBag.msg = msg;
            return View(crewCreate);
            
        }



        //POST: Crew/Create/Validate
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create(string postcode, string HouseNo, string Name, string Type, string Email, string MobNo, DateTime DateOfBirth)
        public IActionResult Create(CrewViewModel FullAddress, string click) //instantiate CrewViewModel object to store form values
        {
            //if (click.Equals("Find"))
            //{
                //Use plugin Net.Http to connect to API, create new object
                using (HttpClient client = new HttpClient())

                {
                    //Store any populated form values in model object called FullAddress, required as View reloads page and input values get blanked
                    string Name = FullAddress.Name;
                    DateTime DateOfBirth = FullAddress.DateOfBirth;
                    string postcode = FullAddress.postcode; //required for address lookup below
                    string HouseNo = FullAddress.HouseNo; //required for address lookup below
                    string Email = FullAddress.Email;
                    string MobNo = FullAddress.MobNo;
                    int CrewId = FullAddress.CrewId;
                    string Type = FullAddress.Type;                    


                    //Connection details for GetAddress.io API that returns full addresses when House No/Name & Postcode provided
                    string apiKey = "3zQi9kpO6E6icIxK75pDxQ11165";
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
                        var JSONaddress = JsonConvert.DeserializeObject<CrewViewModel>(AddressResponse);

                        //GetAddress() API always returns address values as an array with each address line being comma spearated values.
                        //As we're only looking for singular addresses we always use the first and only value in the return array and assign it to a string.
                        string fullAddress = JSONaddress.addresses[0];

                        //Separate address string into an array using comma separator to identify values, also removes any white space.
                        string[] values = fullAddress.Split(',').Select(sValue => sValue.Trim()).ToArray();

                        //Assign array values to associated strings
                        string AddressLine1 = values[0];
                        string AddressLine2 = values[1];
                        string AddressLine3 = values[2];
                        string AddressLine4 = values[3];
                        string Locality = values[4];
                        string TownOrCity = values[5];
                        string County = values[6];

                        //Create array of the strings
                        string[] myStrings = new string[] { AddressLine1, AddressLine2, AddressLine3, AddressLine4, Locality, TownOrCity, County, postcode, null };

                        //Build full address string from populated (not empty/null) values adding line break between each line 
                        //JSONaddress.formattedAddress = string.Join(System.Environment.NewLine, myStrings.Where(str => !string.IsNullOrEmpty(str)));
                        JSONaddress.formattedAddress = string.Join(", ", myStrings.Where(str => !string.IsNullOrEmpty(str)));


                        if (JSONaddress.formattedAddress != null)
                        {
                            //store formattedAddress string in Address field in model object                       
                            FullAddress.Address = JSONaddress.formattedAddress;


                            //Store values to TempData for usage in the CreateRecord action method
                            TempData["Name"] = FullAddress.Name;
                            TempData["DateOfBirth"] = FullAddress.DateOfBirth;                            
                            TempData["Address"] = FullAddress.Address;
                            TempData["MobNo"] = FullAddress.MobNo;
                            TempData["Email"] = FullAddress.Email;                            
                            TempData["CrewId"] = FullAddress.CrewId;
                            TempData["Type"] = FullAddress.Type;

                        //                        
                        return View("CreateRecord", FullAddress);
                        }
                    }
                    
                    //If address not found send following message to View
                    string msg = "Address not found, please try again.";
                    ViewBag.NoAddress = "No Address";
                    ViewBag.msg = msg;                    
                    return View("Create", FullAddress);
                    
                }
            }
        



        // GET: Crew/CreateRecord
        [HttpGet]
        public IActionResult CreateRecord(CrewViewModel FullAddress)
        {
            return View(FullAddress);
        }




         // POST: Crew/CreateRecord
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateUpdateDB([Bind("CrewId,Name,MobNo,Email,Address,DateOfBirth,Type,Status")] Crew crew)
        public IActionResult CreateRecordConfirmed(Crew FullAddress)
        {
            //Convert TempData values back into respective types
            FullAddress.Name = Convert.ToString(TempData["Name"]);
            FullAddress.DateOfBirth = Convert.ToDateTime(TempData["DateOfBirth"]);       
            FullAddress.Address = Convert.ToString(TempData["Address"]);
            FullAddress.MobNo = Convert.ToString(TempData["MobNo"]);
            FullAddress.Email = Convert.ToString(TempData["Email"]);
            //FullAddress.CrewId = Convert.ToInt32(TempData["CrewId"]);
            FullAddress.Type = Convert.ToString(TempData["Type"]);

            //Code below writes form values to SQL DB if ModelState valid 
            //**Removed ModelState validation as passing input view to a review view before submission so data has to be transferred back 
            //**to controller via Viewdata object, not input form therefore ModelState is false by default.

            //var errors = ModelState.Values.SelectMany(v => v.Errors);     
            //if (ModelState.IsValid)
            {
                //Save changes to DB
                _context.Add(FullAddress);
                //Added SQL commands below to allow custom PK values i.e. CrewId (Employee ID) 
                //Didn't work as EF Core 2, tried workaround https://docs.microsoft.com/en-us/ef/core/saving/explicit-values-generated-properties which doesn't work on PK values...losing the will to live!                 
                //_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Crew ON"); 
                _context.SaveChanges();
                //_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Crew OFF");
                _context.Update(FullAddress);
                return RedirectToAction(nameof(Index));
            }
            //return RedirectToAction("Create", FullAddress);
        }
            



        // GET: Crew/Edit/5
        [HttpGet]
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
        public async Task<IActionResult> Edit(int id, [Bind("CrewId,Name,MobNo,Email,Address,DateOfBirth,Type,Status")]Crew crew)
        {            
            if (id != crew.CrewId)
            {
                return NotFound();
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
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

        //Testing using partial views for address lookup form
        //
        //[HttpPost]
        //public ActionResult Contact(string message)
        //{
        //    ViewBag.Message = "Thanks for the Message, we will get back to you";
        //    return PartialView("_CreateAddressLookup");
        //}
    }
}
