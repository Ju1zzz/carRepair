using gotcha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace gotcha.Controllers
{
    public class GuildsController : Controller
    {
        private GuildsEntities db = new GuildsEntities();

        // GET: Guilds
        public ActionResult Index(int pg = 1)
        {
            List<Guild> guilds = db.Guild.ToList();
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

        // GET: Guilds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guild.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            return View(guild);
        }

        // GET: Guilds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guilds/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_guild,nameGuild,firmGuild")] Guild guild)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Guild.Add(guild);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Such guild already exists!");
            }

            return View(guild);
        }

        // GET: Guilds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guild.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            return View(guild);
        }

        // POST: Guilds/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_guild,nameGuild,firmGuild")] Guild guild)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(guild).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Can not be updated! Already exists!");

            }
            return View(guild);
        }

        // GET: Guilds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guild.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            return View(guild);
        }

        // POST: Guilds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guild guild = db.Guild.Find(id);

            try
            {
                db.Guild.Remove(guild);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Can not be deleted! Object is used in another table!");

            }
            return View(guild);
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
