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
    public class ObjectTypeController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /ObjectType/
        public async Task<ActionResult> Index()
        {
            return View(await db.ObjectTypes.ToListAsync());
        }

        // GET: /ObjectType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectType objecttype = await db.ObjectTypes.FindAsync(id);
            if (objecttype == null)
            {
                return HttpNotFound();
            }
            return View(objecttype);
        }

        // GET: /ObjectType/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView("PartialCreate");
            return View();
        }

        // POST: /ObjectType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,ObjectTypeName")] ObjectType objecttype)
        {
            if (ModelState.IsValid)
            {
                db.ObjectTypes.Add(objecttype);
                await db.SaveChangesAsync();
                if (Request.UrlReferrer.PathAndQuery != "/ObjectType/Create")
                {
                    return Redirect(Request.UrlReferrer.PathAndQuery);
                }
                return RedirectToAction("Index");
            }            
            return View(objecttype);
        }

        // GET: /ObjectType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectType objecttype = await db.ObjectTypes.FindAsync(id);
            if (objecttype == null)
            {
                return HttpNotFound();
            }
            return View(objecttype);
        }

        // POST: /ObjectType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,ObjectTypeName")] ObjectType objecttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objecttype).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(objecttype);
        }

        // GET: /ObjectType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectType objecttype = await db.ObjectTypes.FindAsync(id);
            if (objecttype == null)
            {
                return HttpNotFound();
            }
            return View(objecttype);
        }

        // POST: /ObjectType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ObjectType objecttype = await db.ObjectTypes.FindAsync(id);
            db.ObjectTypes.Remove(objecttype);
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
