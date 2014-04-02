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
    public class RegionController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /Region/
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, int? id)
        {
            var viewModel = new RegionIndexData();
            var regions = db.Regions.AsQueryable();

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
                regions = regions.Where(r => r.RegionName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    regions = regions.OrderByDescending(r => r.RegionName);
                    break;
                //case "region":
                //    objectTypes = objectTypes.OrderBy(o => o.Region.RegionName);
                //    break;
                //case "region_desc":
                //    objectTypes = objectTypes.OrderByDescending(m => m.Region.RegionName);
                //    break;
                default:
                    regions = regions.OrderBy(r => r.RegionName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var view = await Task.Run<RegionIndexData>(() =>
            {
                if (id != null)
                {
                    ViewBag.RegionId = id.Value;
                    var region = db.Regions.Where(
                        i => i.Id == id.Value).Single();
                    viewModel.Shelters = region.Shelters;
                    viewModel.MountainRanges = region.MountainRanges;
                    ViewBag.RegionName = region.RegionName;
                }
                viewModel.Regions = regions.ToPagedList(pageNumber, pageSize);
                return viewModel;
            });

            return View(view);
            //return View(await db.Regions.ToListAsync());
        }

        // GET: /Region/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: /Region/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView("PartialCreate");
            return View();
        }

        // POST: /Region/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="RegionName")] Region region)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Regions.Add(region);
                    await db.SaveChangesAsync();
                    await db.SaveRecord(region, User.Identity.Name, "New region added");
                    if (Request.UrlReferrer.PathAndQuery != "/Region/Create")
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
            return View(region);
        }

        // GET: /Region/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: /Region/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,RegionName")] Region region)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(region).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    await db.SaveRecord(region, User.Identity.Name, "Region modified");
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(region);
        }

        // GET: /Region/Delete/5
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
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: /Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Region region = await db.Regions.FindAsync(id);
                db.Regions.Remove(region);
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

        /// <summary>
        /// Filter classes based on student id
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        // GET: /Region/LoadRangesByRegionId?regionId=5
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> LoadRangesByRegionId(string regionId)
        {
            var ranges = new List<MountainRange>();
            if (regionId != "")
            {
                Region region = await db.Regions.FindAsync(Convert.ToInt32(regionId));
                ranges = region.MountainRanges.ToList();
            }
            else
            {
                ranges = await db.MountainRanges.ToListAsync();
            }
            var rangesData = ranges.Select(m => new SelectListItem()
            {
                Text = m.MountainRangeName,
                Value = m.Id.ToString(),
            });
            return Json(rangesData, JsonRequestBehavior.AllowGet);
        }
    }
}
