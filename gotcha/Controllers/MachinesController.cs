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
    public class MachinesController : Controller
    {
        private GuildsEntities db = new GuildsEntities();

        // GET: Machines
        public ActionResult Index(int pg = 1)
        {
        
            List<Machine> machines = db.Machine.ToList();
            const int pageSize = 50;
            if (pg < 1)
                pg = 1;

            int rescCount = machines.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = machines.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages; 
            return View(data);
               
        }

        // GET: Machines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machine.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // GET: Machines/Create
        public ActionResult Create()
        {
            ViewBag.fk_id_guild = new SelectList(db.Guild, "id_guild", "id_guild");
            return View();
        }

        // POST: Machines/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mach,fk_id_guild,firmMach,timeProcessing")] Machine machine)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Machine.Add(machine);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.fk_id_guild = new SelectList(db.Guild, "id_guild", "id_guild", machine.fk_id_guild);
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Such machine already exists!");
            }

            return View(machine);
        }

        // GET: Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machine.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_id_guild = new SelectList(db.Guild, "id_guild", "id_guild", machine.fk_id_guild);
            return View(machine);
        }

        // POST: Machines/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mach,fk_id_guild,firmMach,timeProcessing")] Machine machine)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(machine).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.fk_id_guild = new SelectList(db.Guild, "id_guild", "id_guild", machine.fk_id_guild);

            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Can not be updated! Already exists!");

            }
            return View(machine);
        }

        // GET: Machines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machine.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine machine = db.Machine.Find(id);

            try
            {
                db.Machine.Remove(machine);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ошибка! Объект используется в другой таблице");
            }
            return View(machine);
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
