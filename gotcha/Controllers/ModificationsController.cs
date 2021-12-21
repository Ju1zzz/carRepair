using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using gotcha.Models;

namespace gotcha.Controllers
{
    public class ModificationsController : Controller
    {
        private GuildsEntities db = new GuildsEntities();

        // GET: Modifications
        public ActionResult Index(int pg = 1)
        {
            List<Modification> modifications = db.Modification.ToList();
            const int pageSize = 30;
            if (pg < 1)
                pg = 1;

            int rescCount = modifications.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = modifications.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);

        }

        // GET: Modifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modification modification = db.Modification.Find(id);
            if (modification == null)
            {
                return HttpNotFound();
            }
            return View(modification);
        }

        // GET: Modifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modifications/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mod,nameMod,LabourInput")] Modification modification)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Modification.Add(modification);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Such modification already exists!");
            }

            return View(modification);
        }

        // GET: Modifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modification modification = db.Modification.Find(id);
            if (modification == null)
            {
                return HttpNotFound();
            }
            return View(modification);
        }

        // POST: Modifications/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mod,nameMod,LabourInput")] Modification modification)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(modification).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Can not be updated! Already exists!");

            }
            return View(modification);
        }


        // GET: Modifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modification modification = db.Modification.Find(id);
            if (modification == null)
            {
                return HttpNotFound();
            }
            return View(modification);

        }

        // POST: Modifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modification modification = db.Modification.Find(id);
            try
            {
                db.Modification.Remove(modification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Can not be deleted! Object is used in another table!");

            }
            return View(modification);

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
