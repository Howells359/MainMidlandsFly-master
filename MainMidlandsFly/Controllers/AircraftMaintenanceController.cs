using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MainMidlandsFly.Data;
using MainMidlandsFly.Models;
using System.Net;

namespace MainMidlandsFly.Controllers
{
    public class AircraftMaintenanceController : Controller
    {
        AircraftMaintenanceContext db ;

        public AircraftMaintenanceController(AircraftMaintenanceContext context)
        {
            db = context;
        }

        // GET: AircraftMaintenance
        public ActionResult Index()
        {


            var result = db.Aircraft
    .Join(db.AircraftMaintenance, a => a.ID, am => am.AircraftId, (a, am) => new { a, am })
    .Join(db.Crew, amc => amc.am.Ground_Crew_Id, c => c.CrewId, (amc, c) => new { amc, c })
    .Select(m => new AircraftMaintenanceModel
    {
        ID = m.amc.am.ID,
        AircraftId = m.amc.am.AircraftId,
        Ground_Crew_Id = m.amc.am.Ground_Crew_Id,
        AircraftRegNum = m.amc.a.AircraftRegNo, // or m.ppc.pc.ProdId
        CrewMemberName = m.c.Name,
        Maintenance_History = m.amc.am.Maintenance_History,
        Date = m.amc.am.Date,
        Job_Status = m.amc.am.Job_Status

    }).ToList();

            return View(result);
        }

        // GET: AircraftMaintenance/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AircraftMaintenance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AircraftMaintenance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AircraftMaintenance/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                
             //  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result =
       from am in db.AircraftMaintenance
       join a in db.Aircraft on am.AircraftId equals a.ID
       join c in db.Crew on am.Ground_Crew_Id equals c.CrewId
       select new AircraftMaintenanceModel
       { ID = am.ID,
           AircraftId = am.AircraftId,
           Ground_Crew_Id = am.Ground_Crew_Id,
           AircraftRegNum = a.AircraftRegNo, // or m.ppc.pc.ProdId
           CrewMemberName = c.Name,
           Maintenance_History = am.Maintenance_History,
           Date = am.Date,
           Job_Status = am.Job_Status
       };



    //        var result = db.Aircraft
    //.Join(db.AircraftMaintenance, a => a.ID, am => am.AircraftId, (a, am) => new { a, am })
    //.Where(amp => amp.am.ID == id)
    //.Join(db.Crew, amc => amc.am.Ground_Crew_Id, c => c.CrewId, (amc, c) => new { amc, c })
    
    //.Select(m => new AircraftMaintenance
    //{
    //    AircraftRegNum = m.amc.a.AircraftRegNo, // or m.ppc.pc.ProdId
    //    CrewMemberName = m.c.Name,
    //    Maintenance_History = m.amc.am.Maintenance_History,
    //    Date = m.amc.am.Date,
    //    Job_Status = m.amc.am.Job_Status

    //}) .FirstOrDefault();

          // AircraftMaintenance obj = db.AircraftMaintenance.Find(id);
            //if (result == null)
            //{
            //    return View();
            //  //  return HttpNotFound();
            //}
            return View(result.FirstOrDefault(i => i.ID == id));

            
        }

        // POST: AircraftMaintenance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                AircraftMaintenance am = new AircraftMaintenance();

                am.ID = Int32.Parse(collection["ID"]);
                am.AircraftId = Int32.Parse(collection["AircraftId"]);
                am.Ground_Crew_Id = Int32.Parse(collection["Ground_Crew_Id"]);
                am.Maintenance_History = collection["Maintenance_History"].ToString();
                am.Job_Status = collection["Job_Status"].ToString();
                am.Date = DateTime.Parse(collection["Date"].ToString());


                db.Update(am);
                db.SaveChanges();


                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
                return View();
            }
        }

        // GET: AircraftMaintenance/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AircraftMaintenance/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}