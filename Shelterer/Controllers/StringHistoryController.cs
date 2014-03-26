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
    public class StringHistoryController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /StringHistory/
        public async Task<ActionResult> Index()
        {
            return View(await db.StringHistory.ToListAsync());
        }

        // GET: /StringHistory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StringHistory stringhistory = await db.StringHistory.FindAsync(id);
            if (stringhistory == null)
            {
                return HttpNotFound();
            }
            return View(stringhistory);
        }

        // GET: /StringHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StringHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Record,Value,ShelterId,Author,Message,StartDate,EndDate")] StringHistory stringhistory)
        {
            if (ModelState.IsValid)
            {
                db.StringHistory.Add(stringhistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stringhistory);
        }

        // GET: /StringHistory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StringHistory stringhistory = await db.StringHistory.FindAsync(id);
            if (stringhistory == null)
            {
                return HttpNotFound();
            }
            return View(stringhistory);
        }

        // POST: /StringHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Record,Value,ShelterId,Author,Message,StartDate,EndDate")] StringHistory stringhistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stringhistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stringhistory);
        }

        // GET: /StringHistory/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StringHistory stringhistory = await db.StringHistory.FindAsync(id);
            if (stringhistory == null)
            {
                return HttpNotFound();
            }
            return View(stringhistory);
        }

        // POST: /StringHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StringHistory stringhistory = await db.StringHistory.FindAsync(id);
            db.StringHistory.Remove(stringhistory);
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
