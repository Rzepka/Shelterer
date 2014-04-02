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
using Shelterer.ViewModels;
using PagedList;

namespace Shelterer.Controllers
{
    public class ObjectTypeController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /ObjectType/
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, int? id)
        {
            var viewModel = new ObjectTypeIndexData();
            var objectTypes = db.ObjectTypes.AsQueryable();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                objectTypes = objectTypes.Where(o => o.ObjectTypeName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    objectTypes = objectTypes.OrderByDescending(o => o.ObjectTypeName);
                    break;
                //case "region":
                //    objectTypes = objectTypes.OrderBy(o => o.Region.RegionName);
                //    break;
                //case "region_desc":
                //    objectTypes = objectTypes.OrderByDescending(m => m.Region.RegionName);
                //    break;
                default:
                    objectTypes = objectTypes.OrderBy(o => o.ObjectTypeName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var view = await Task.Run<ObjectTypeIndexData>(() =>
            {
                if (id != null)
                {
                    ViewBag.ObjectTypeId = id.Value;
                    var type = db.ObjectTypes.Where(
                        i => i.Id == id.Value).Single();
                    viewModel.Shelters = type.Shelters;
                    ViewBag.ObjectTypeName = type.ObjectTypeName;
                }
                viewModel.ObjectTypes = objectTypes.ToPagedList(pageNumber, pageSize);
                return viewModel;
            });

            return View(view);
            //return View(await db.ObjectTypes.ToListAsync());
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
        public async Task<ActionResult> Create([Bind(Include="ObjectTypeName")] ObjectType objecttype)
        {
            try
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
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(objecttype).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(objecttype);
        }

        // GET: /ObjectType/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
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
            try
            {
                ObjectType objecttype = await db.ObjectTypes.FindAsync(id);
                db.ObjectTypes.Remove(objecttype);
                await db.SaveChangesAsync();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
