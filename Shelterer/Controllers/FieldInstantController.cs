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
using SheltererExtensionMethods;

namespace Shelterer.Controllers
{
    [Authorize]
    public class FieldInstantController : Controller
    {
        private SheltersContext db = new SheltersContext();

        // GET: /FieldInstant/
        public async Task<ActionResult> Index(string table, string field, int? recordId, int? tableId, int? fieldId)
        {
            var viewModel = new FieldInstantIndexData();

            try
            {
                if (table != null && table != "")
                {
                    tableId = DbRecord.TableFieldIds[table].Item1;
                    if (field != null && field != "")
                        fieldId = DbRecord.TableFieldIds[table].Item2[field];
                }
            }
            catch (KeyNotFoundException)
            {
                return HttpNotFound();
            }
            viewModel.FieldInstants = await db.FieldInstants
                .Where(i => recordId == null || i.RecordId == recordId)
                .Where(i => fieldId == null || i.FieldId == fieldId)
                .Where(i => tableId == null || i.TableId == tableId)
                .ToListAsync();
            if (Request.IsAjaxRequest())
                return PartialView("PartialIndex", viewModel);
            return View(viewModel);
        }

        //[HttpPost]
        public async Task<ActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldInstant fieldinstant = await db.FieldInstants.FindAsync(id);
            if (fieldinstant == null)
            {
                return HttpNotFound();
            }

            var tuple = await db.RetriveProperty(fieldinstant, User.Identity.Name, "Previous value restored");

            return RedirectToAction("Details", tuple.Item2, new{id = tuple.Item1.Id});
        }

        // GET: /FieldInstant/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldInstant fieldinstant = await db.FieldInstants.FindAsync(id);
            if (fieldinstant == null)
            {
                return HttpNotFound();
            }
            return View(fieldinstant);
        }

        //// GET: /FieldInstant/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /FieldInstant/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include="Id,TableId,RecordId,FieldId,DataTypeId,Value,Author,Message,StartDate,EndDate")] FieldInstant fieldinstant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.FieldInstants.Add(fieldinstant);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(fieldinstant);
        //}

        //// GET: /FieldInstant/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FieldInstant fieldinstant = await db.FieldInstants.FindAsync(id);
        //    if (fieldinstant == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(fieldinstant);
        //}

        //// POST: /FieldInstant/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include="Id,TableId,RecordId,FieldId,DataTypeId,Value,Author,Message,StartDate,EndDate")] FieldInstant fieldinstant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(fieldinstant).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(fieldinstant);
        //}

        //// GET: /FieldInstant/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FieldInstant fieldinstant = await db.FieldInstants.FindAsync(id);
        //    if (fieldinstant == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(fieldinstant);
        //}

        //// POST: /FieldInstant/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    FieldInstant fieldinstant = await db.FieldInstants.FindAsync(id);
        //    db.FieldInstants.Remove(fieldinstant);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
