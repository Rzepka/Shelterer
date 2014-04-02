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
using PagedList;
using Shelterer.ViewModels;
using SheltererExtensionMethods;

namespace Shelterer.Controllers
{
    [Authorize]
    public class MountainRangeController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /MountainRange/
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, int? id)
        {
            var viewModel = new MountainRangeIndexData();
            var mountainranges = db.MountainRanges.Include(m => m.Region);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.RegionSortParm = sortOrder == "region" ? "region_desc" : "region";
            
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
                mountainranges = mountainranges.Where(m => m.MountainRangeName.ToUpper().Contains(searchString.ToUpper())
                                       || m.Region.RegionName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    mountainranges = mountainranges.OrderByDescending(m => m.MountainRangeName);
                    break;
                case "region":
                    mountainranges = mountainranges.OrderBy(m => m.Region.RegionName);
                    break;
                case "region_desc":
                    mountainranges = mountainranges.OrderByDescending(m => m.Region.RegionName);
                    break;
                default:
                    mountainranges = mountainranges.OrderBy(m => m.MountainRangeName);
                    break;
            }
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var view = await Task.Run<MountainRangeIndexData>(() =>
            {
                if (id != null)
                {
                    ViewBag.MountainRangeId = id.Value;
                    var range = db.MountainRanges.Where(
                        i => i.Id == id.Value).Single();
                    viewModel.Shelters = range.Shelters;
                    ViewBag.MountainRangeName = range.MountainRangeName;
                }
                viewModel.MountainRanges = mountainranges.ToPagedList(pageNumber, pageSize);
                return viewModel;
            });

            return View(view);
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
            if (Request.IsAjaxRequest())
                return PartialView("PartialCreate");
            return View();
        }

        // POST: /MountainRange/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="MountainRangeName,RegionId")] MountainRange mountainrange)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MountainRanges.Add(mountainrange);
                    await db.SaveChangesAsync();
                    await db.SaveRecord(mountainrange, User.Identity.Name, "New mountain range added");
                    if (Request.UrlReferrer.PathAndQuery != "/MountainRange/Create")
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(mountainrange).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    await db.SaveRecord(mountainrange, User.Identity.Name, "Mountain range modified");
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", mountainrange.RegionId);
            return View(mountainrange);
        }

        // GET: /MountainRange/Delete/5
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
            MountainRange mountainrange = await db.MountainRanges.FindAsync(id);
            if (mountainrange == null)
            {
                return HttpNotFound();
            }
            return View(mountainrange);
        }

        // POST: /MountainRange/Delete/5
        [HttpPost]//, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)//DeleteConfirmed(int id)
        {
            try
            {
                MountainRange mountainrange = await db.MountainRanges.FindAsync(id);
                db.MountainRanges.Remove(mountainrange);
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
