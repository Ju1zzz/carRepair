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
    public class ProcessingsController : Controller
    {
        private GuildsEntities db = new GuildsEntities();

        // GET: Processings
        public ActionResult Index(int pg =1)
        {
            List<Processing> processing = db.Processing.ToList();
            const int pageSize = 30;
            if (pg < 1)
                pg = 1;

            int rescCount = processing.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = processing.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: Processings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processing processing = db.Processing.Find(id);
            if (processing == null)
            {
                return HttpNotFound();
            }
            return View(processing);
        }

        // GET: Processings/Create
        public ActionResult Create()
        {
            ViewBag.fk_id_mod = new SelectList(db.Modification, "id_mod", "id_mod");
            ViewBag.fk_id_part = new SelectList(db.Part, "id_part", "id_part");
            return View();
        }

        // POST: Processings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_row,fk_id_part,fk_id_mod")] Processing processing)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    db.Processing.Add(processing);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.fk_id_mod = new SelectList(db.Modification, "id_mod", "id_mod", processing.fk_id_mod);
                ViewBag.fk_id_part = new SelectList(db.Part, "id_part", "id_part", processing.fk_id_part);

            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Such processing already exists!");
            }

            return View(processing);
        }

        // GET: Processings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processing processing = db.Processing.Find(id);
            if (processing == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_id_mod = new SelectList(db.Modification, "id_mod", "id_mod", processing.fk_id_mod);
            ViewBag.fk_id_part = new SelectList(db.Part, "id_part", "id_part", processing.fk_id_part);
            return View(processing);
        }

        // POST: Processings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_row,fk_id_part,fk_id_mod")] Processing processing)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(processing).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.fk_id_mod = new SelectList(db.Modification, "id_mod", "id_mod", processing.fk_id_mod);
                ViewBag.fk_id_part = new SelectList(db.Part, "id_part", "id_part", processing.fk_id_part);


            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Such processing already exists!");
            }
            return View(processing);
        }

        // GET: Processings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processing processing = db.Processing.Find(id);
            if (processing == null)
            {
                return HttpNotFound();
            }
            return View(processing);
        }

        // POST: Processings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Processing processing = db.Processing.Find(id);
            db.Processing.Remove(processing);
            db.SaveChanges();
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
