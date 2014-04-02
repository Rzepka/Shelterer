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
using SheltererExtensionMethods;
using Shelterer.ViewModels;
using PagedList;

namespace Shelterer.Controllers
{
    public class ShelterController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /Shelter/
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, int? id)
        {
            var shelters = db.Shelters.Include(s => s.MountainRange).Include(s => s.ObjectType).Include(s => s.Region);

            var viewModel = new ShelterIndexData();
            //var shelters = db.Shelters.AsQueryable();

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
                shelters = shelters.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    shelters = shelters.OrderByDescending(o => o.Name);
                    break;
                //case "region":
                //    objectTypes = objectTypes.OrderBy(o => o.Region.RegionName);
                //    break;
                //case "region_desc":
                //    objectTypes = objectTypes.OrderByDescending(m => m.Region.RegionName);
                //    break;
                default:
                    shelters = shelters.OrderBy(o => o.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var view = await Task.Run<ShelterIndexData>(() =>
            {
                if (id != null)
                {
                    ViewBag.ShelterId = id.Value;
                    var shelter = db.Shelters.Where(
                        i => i.Id == id.Value).Single();
                    viewModel.Images = shelter.Images.Take(4);
                    ViewBag.ShelterName = shelter.Name;
                }
                viewModel.Shelters = shelters.ToPagedList(pageNumber, pageSize);
                return viewModel;
            });

            return View(view);
            //return View(await shelters.ToListAsync());
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
        public async Task<ActionResult> Create([Bind(Include="Id,Name,ObjectTypeId,Altitude,Latitude,Longitude,RegionId,MountainRangeId,Visited,Capacity,Owner,Opening,Location,TechnicalCondition,Remarks,WaterAccess,Fireplace,LastUpdate")] Shelter shelter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Shelters.Add(shelter);
                    await db.SaveChangesAsync();
                    //TODO: author and message
                    await db.SaveRecord(shelter, "zenek", "elo");
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "MountainRangeName", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(db.ObjectTypes, "Id", "ObjectTypeName", shelter.ObjectTypeId);
            //ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", shelter.RegionId);
            ViewBag.MountainRangeId = db.MountainRanges.ToList();
            ViewBag.RegionId = db.Regions.ToList();
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
            //ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "MountainRangeName", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(await db.ObjectTypes.ToListAsync(), "Id", "ObjectTypeName", shelter.ObjectTypeId);
            //ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", shelter.RegionId);
            ViewBag.MountainRangeId = await db.MountainRanges.ToListAsync();
            ViewBag.RegionId = await db.Regions.ToListAsync();
            return View(shelter);
        }

        // POST: /Shelter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,ObjectTypeId,Altitude,Latitude,Longitude,RegionId,MountainRangeId,Visited,Capacity,Owner,Opening,Location,TechnicalCondition,Remarks,WaterAccess,Fireplace,LastUpdate")] Shelter shelter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(shelter).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    //TODO: author and message
                    await db.SaveRecord(shelter, "mietek", "siemka");
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //ViewBag.MountainRangeId = new SelectList(db.MountainRanges, "Id", "MountainRangeName", shelter.MountainRangeId);
            ViewBag.ObjectTypeId = new SelectList(await db.ObjectTypes.ToListAsync(), "Id", "ObjectTypeName", shelter.ObjectTypeId);
            //ViewBag.RegionId = new SelectList(db.Regions, "Id", "RegionName", shelter.RegionId);
            ViewBag.MountainRangeId = await db.MountainRanges.ToListAsync();
            ViewBag.RegionId = await db.Regions.ToListAsync();
            return View(shelter);
        }

        // GET: /Shelter/Delete/5
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
            try
            {
                Shelter shelter = await db.Shelters.FindAsync(id);
            db.Shelters.Remove(shelter);
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


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> LoadImagesByShelterId(string shelterId)
        {
            var images = new List<Image>();
            if (shelterId != "")
            {
                Shelter shelter = await db.Shelters.FindAsync(Convert.ToInt32(shelterId));
                images = shelter.Images.ToList();
            }
            else
            {
                images = await db.Images.ToListAsync();
            }
            var imagesData = images.Select(i => i.Id.ToString());

            return Json(imagesData, JsonRequestBehavior.AllowGet);
        }

    }
}
