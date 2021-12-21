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
    public class DraftsController : Controller
    {
        private GuildsEntities db = new GuildsEntities();

        // GET: Drafts
        public ActionResult Index(int pg = 1)
        {
            List<Draft> guilds = db.Draft.ToList();
            const int pageSize = 30;
            if (pg < 1)
                pg = 1;

            int rescCount = guilds.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = guilds.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: Drafts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draft draft = db.Draft.Find(id);
            if (draft == null)
            {
                return HttpNotFound();
            }
            return View(draft);
        }

        // GET: Drafts/Create
        public ActionResult Create()
        {
            ViewBag.fk_id_mach = new SelectList(db.Machine, "id_mach", "id_mach");
            ViewBag.fk_id_part = new SelectList(db.Part, "id_part", "id_part");
            return View();
        }

        // POST: Drafts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_row,fk_id_part,fk_id_mach,version")] Draft draft)
        {
            string ex = "";
            try
            {
                if (ModelState.IsValid)
                {
                    db.Draft.Add(draft);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } else if (draft.fk_id_mach > db.Machine.Count() || draft.fk_id_part > db.Part.Count())
                {
                    ex = "You use too large number";
                }
                else
                {
                    ex = "Such draft already exists!";
                }

                ViewBag.fk_id_mach = new SelectList(db.Machine, "id_mach", "id_mach", draft.fk_id_mach);
                ViewBag.fk_id_part = new SelectList(db.Part, "id_part", "id_part", draft.fk_id_part);

            }
            catch (Exception)
            {

                ModelState.AddModelError("", ex);
            }

            return View(draft);
           
        }

        // GET: Drafts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draft draft = db.Draft.Find(id);
            if (draft == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_id_mach = new SelectList(db.Machine, "id_mach", "id_mach", draft.fk_id_mach);
            ViewBag.fk_id_part = new SelectList(db.Part, "id_part", "id_part", draft.fk_id_part);
            return View(draft);
        }

        // POST: Drafts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_row,fk_id_part,fk_id_mach,version")] Draft draft)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(draft).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.fk_id_mach = new SelectList(db.Machine, "id_mach", "id_mach", draft.fk_id_mach);
                ViewBag.fk_id_part = new SelectList(db.Part, "id_part", "id_part", draft.fk_id_part);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Such draft already exists!");
            }

            return View(draft);
        }

        // GET: Drafts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draft draft = db.Draft.Find(id);
            if (draft == null)
            {
                return HttpNotFound();
            }
            return View(draft);
        }

        // POST: Drafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Draft draft = db.Draft.Find(id);
            db.Draft.Remove(draft);
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
