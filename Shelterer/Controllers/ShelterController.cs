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

namespace Shelterer.Controllers
{
    public class ShelterController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /Shelter/
        public async Task<ActionResult> Index()
        {
            var shelters = db.Shelters.Include(s => s.MountainRange).Include(s => s.ObjectType);
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
            ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "Name");
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "Name");
            return View();
        }

        // POST: /Shelter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,ObjectTypeId,Altitude,MountainRangeId,Visited,Owner,Opening,Location,TechnicalCondition,Remarks,WaterAccess,Fireplace,LastUpdate")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                db.Shelters.Add(shelter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "Name", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "Name", shelter.ObjectTypeId);
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
            ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "Name", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "Name", shelter.ObjectTypeId);
            return View(shelter);
        }

        // POST: /Shelter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,ObjectTypeId,Altitude,MountainRangeId,Visited,Owner,Opening,Location,TechnicalCondition,Remarks,WaterAccess,Fireplace,LastUpdate")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shelter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "Name", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "Name", shelter.ObjectTypeId);
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
    }
}
