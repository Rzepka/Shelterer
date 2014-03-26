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
    public class MountainRangeController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /MountainRange/
        public async Task<ActionResult> Index()
        {
            var mountainranges = db.MountainRanges.Include(m => m.Region);
            return View(await mountainranges.ToListAsync());
        }

        // GET: /MountainRange/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MountainRange mountainrange = await db.MountainRanges.FindAsync(id);
            if (mountainrange == null)
            {
                return HttpNotFound();
            }
            return View(mountainrange);
        }

        // GET: /MountainRange/Create
        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName");
            return View();
        }

        // POST: /MountainRange/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,MountainRangeName,RegionId")] MountainRange mountainrange)
        {
            if (ModelState.IsValid)
            {
                db.MountainRanges.Add(mountainrange);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", mountainrange.RegionId);
            return View(mountainrange);
        }

        // GET: /MountainRange/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MountainRange mountainrange = await db.MountainRanges.FindAsync(id);
            if (mountainrange == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", mountainrange.RegionId);
            return View(mountainrange);
        }

        // POST: /MountainRange/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,MountainRangeName,RegionId")] MountainRange mountainrange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mountainrange).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", mountainrange.RegionId);
            return View(mountainrange);
        }

        // GET: /MountainRange/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MountainRange mountainrange = await db.MountainRanges.FindAsync(id);
            if (mountainrange == null)
            {
                return HttpNotFound();
            }
            return View(mountainrange);
        }

        // POST: /MountainRange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MountainRange mountainrange = await db.MountainRanges.FindAsync(id);
            db.MountainRanges.Remove(mountainrange);
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
