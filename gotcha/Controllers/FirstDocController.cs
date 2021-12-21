using gotcha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gotcha.Controllers
{
    public class FirstDocController : Controller
    {
        private GuildsEntities db = new GuildsEntities();

        public ActionResult Index(int pg = 1)
        {
            List<FirstDoc_Result> requests = db.Database.SqlQuery<FirstDoc_Result>("FirstDoc").ToList();
            const int pageSize = 30;
            if (pg < 1)
                pg = 1;

            int rescCount = requests.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = requests.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
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