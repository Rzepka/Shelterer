using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shelterer.Models;
using System.Reflection;
using Shelterer.Models.History;

namespace Shelterer.Controllers
{
    public class ShelterController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /Shelter/
        public async Task<ActionResult> Index()
        {
            var shelters = db.Shelters.Include(s => s.MountainRange).Include(s => s.ObjectType).Include(s => s.Region);
            return View(await shelters.ToListAsync());
        }

        // GET: /Shelter/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelter shelter = await db.Shelters.FindAsync(id);
            if (shelter == null)
            {
                return HttpNotFound();
            }
            return View(shelter);
        }

        // GET: /Shelter/Create
        public ActionResult Create()
        {
            //ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "MountainRangeName");
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "ObjectTypeName");
            //ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName");
            ViewBag.MountainRangeId = db.MountainRanges.ToList();
            ViewBag.RegionId = db.Regions.ToList();
            return View();
        }

        // POST: /Shelter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,ObjectTypeId,Altitude,Latitude,Longitude,RegionId,MountainRangeId,Visited,Owner,Opening,Location,TechnicalCondition,Remarks,WaterAccess,Fireplace,LastUpdate")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                db.Shelters.Add(shelter);
                await db.SaveChangesAsync();
                //TODO: author and message
                await SaveRecords(shelter, null, "zenek", "elo");
                return RedirectToAction("Index");
            }

            ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "MountainRangeName", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "ObjectTypeName", shelter.ObjectTypeId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", shelter.RegionId);
            return View(shelter);
        }

        // GET: /Shelter/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelter shelter = await db.Shelters.FindAsync(id);
            if (shelter == null)
            {
                return HttpNotFound();
            }
            ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "MountainRangeName", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "ObjectTypeName", shelter.ObjectTypeId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", shelter.RegionId);
            return View(shelter);
        }

        // POST: /Shelter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,ObjectTypeId,Altitude,Latitude,Longitude,RegionId,MountainRangeId,Visited,Owner,Opening,Location,TechnicalCondition,Remarks,WaterAccess,Fireplace,LastUpdate")] Shelter shelter)
        {
            Shelter oldShelter = await db.Shelters.FindAsync(shelter.Id);
            if (ModelState.IsValid)
            {
                db.Entry(shelter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //TODO: author and message
                await SaveRecords(shelter, oldShelter, "mietek", "siemka");
                return RedirectToAction("Index");
            }
            ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "MountainRangeName", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "ObjectTypeName", shelter.ObjectTypeId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", shelter.RegionId);
            return View(shelter);
        }

        // GET: /Shelter/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelter shelter = await db.Shelters.FindAsync(id);
            if (shelter == null)
            {
                return HttpNotFound();
            }
            return View(shelter);
        }

        // POST: /Shelter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Shelter shelter = await db.Shelters.FindAsync(id);
            db.Shelters.Remove(shelter);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        public async Task SaveRecords(Shelter newShelter, Shelter old, string author, string message)
        {
            //get all properties
            var properties = newShelter.GetType().GetProperties();
            //check if new shelter is added or edited
            if (old == null)
            {
                foreach (var p in properties)
                {   //save all properties
                    await StoreProperty(p, newShelter, author, message, false);
                }
            }
            else
            {
                foreach (var p in properties)
                {   //save only changed properties
                    var newVal = p.GetValue(newShelter);
                    var oldVal = p.GetValue(old);                    
                    if (! newVal.Equals(oldVal))
                    {
                        await StoreProperty(p, newShelter, author, message, true);
                    }

                }
            }
            
        }

        //store single property indicated by info
        public async Task StoreProperty(PropertyInfo info, Shelter shelter, string author, string message, bool close)
        {
            string type = info.PropertyType.Name;

            //check the type of property
            if (type.Contains("Int"))
            {
                string name = info.Name;
                //dont save id of the object
                if (name == "Id")
                    return;
                int value = (int)info.GetValue(shelter);
                //select column
                int record = (int)Enum.Parse(typeof(IntRecords), name);
                var newRecord = new IntHistory()
                {
                    ShelterId = shelter.Id,
                    Record = record,
                    Author = author,
                    Message = message,
                    StartDate = DateTime.Now,
                    Value = value
                };
                db.IntHistory.Add(newRecord);

                if (close)
                {   //close old record
                    IntHistory oldRecord = await db.IntHistory.FirstAsync(r =>
                        r.ShelterId == shelter.Id &&
                        r.Record == record &&
                        r.EndDate == null);                    
                    oldRecord.EndDate = DateTime.Now;
                    db.Entry(oldRecord).State = EntityState.Modified;
                }
            }
            else if (type.Contains("String"))
            {
                string name = info.Name;
                string value = (string)info.GetValue(shelter);
                int record = (int)Enum.Parse(typeof(StringRecords), name);
                var newRecord = new StringHistory()
                {
                    ShelterId = shelter.Id,
                    Record = record,
                    Author = author,
                    Message = message,
                    StartDate = DateTime.Now,
                    Value = value
                };
                db.StringHistory.Add(newRecord);

                if (close)
                {
                    StringHistory oldRecord = await db.StringHistory.FirstAsync(r =>
                        r.ShelterId == shelter.Id &&
                        r.Record == record &&
                        r.EndDate == null);
                    oldRecord.EndDate = DateTime.Now;
                    db.Entry(oldRecord).State = EntityState.Modified;
                }
            }
            else if (type.Contains("Double"))
            {
                string name = info.Name;
                double value = (double)info.GetValue(shelter);
                int record = (int)Enum.Parse(typeof(DoubleRecords), name);
                var newRecord = new DoubleHistory()
                {
                    ShelterId = shelter.Id,
                    Record = record,
                    Author = author,
                    Message = message,
                    StartDate = DateTime.Now,
                    Value = value
                };
                db.DoubleHistory.Add(newRecord);

                if (close)
                {
                    DoubleHistory oldRecord = await db.DoubleHistory.FirstAsync(r =>
                        r.ShelterId == shelter.Id &&
                        r.Record == record &&
                        r.EndDate == null);
                    oldRecord.EndDate = DateTime.Now;
                    db.Entry(oldRecord).State = EntityState.Modified;
                }
            }
            else //object properties are not stored
                return;
            await db.SaveChangesAsync();
        }
    
    }
}
