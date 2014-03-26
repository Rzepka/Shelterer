using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shelterer.Models.History;
using Shelterer.Models;

namespace Shelterer.Controllers
{
    public class DoubleHistoryController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /DoubleHistory/
        public async Task<ActionResult> Index()
        {
            return View(await db.DoubleHistory.ToListAsync());
        }

        // GET: /DoubleHistory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoubleHistory doublehistory = await db.DoubleHistory.FindAsync(id);
            if (doublehistory == null)
            {
                return HttpNotFound();
            }
            return View(doublehistory);
        }

        // GET: /DoubleHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DoubleHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Record,Value,ShelterId,Author,Message,StartDate,EndDate")] DoubleHistory doublehistory)
        {
            if (ModelState.IsValid)
            {
                db.DoubleHistory.Add(doublehistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(doublehistory);
        }

        // GET: /DoubleHistory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoubleHistory doublehistory = await db.DoubleHistory.FindAsync(id);
            if (doublehistory == null)
            {
                return HttpNotFound();
            }
            return View(doublehistory);
        }

        // POST: /DoubleHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Record,Value,ShelterId,Author,Message,StartDate,EndDate")] DoubleHistory doublehistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doublehistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(doublehistory);
        }

        // GET: /DoubleHistory/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoubleHistory doublehistory = await db.DoubleHistory.FindAsync(id);
            if (doublehistory == null)
            {
                return HttpNotFound();
            }
            return View(doublehistory);
        }

        // POST: /DoubleHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DoubleHistory doublehistory = await db.DoubleHistory.FindAsync(id);
            db.DoubleHistory.Remove(doublehistory);
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
