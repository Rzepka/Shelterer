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
    public class IntHistoryController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /IntHistory/
        public async Task<ActionResult> Index()
        {
            return View(await db.IntHistory.ToListAsync());
        }

        // GET: /IntHistory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntHistory inthistory = await db.IntHistory.FindAsync(id);
            if (inthistory == null)
            {
                return HttpNotFound();
            }
            return View(inthistory);
        }

        // GET: /IntHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /IntHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Record,Value,ShelterId,Author,Message,StartDate,EndDate")] IntHistory inthistory)
        {
            if (ModelState.IsValid)
            {
                db.IntHistory.Add(inthistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(inthistory);
        }

        // GET: /IntHistory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntHistory inthistory = await db.IntHistory.FindAsync(id);
            if (inthistory == null)
            {
                return HttpNotFound();
            }
            return View(inthistory);
        }

        // POST: /IntHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Record,Value,ShelterId,Author,Message,StartDate,EndDate")] IntHistory inthistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inthistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(inthistory);
        }

        // GET: /IntHistory/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntHistory inthistory = await db.IntHistory.FindAsync(id);
            if (inthistory == null)
            {
                return HttpNotFound();
            }
            return View(inthistory);
        }

        // POST: /IntHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IntHistory inthistory = await db.IntHistory.FindAsync(id);
            db.IntHistory.Remove(inthistory);
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
