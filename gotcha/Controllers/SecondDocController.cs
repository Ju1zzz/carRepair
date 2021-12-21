using gotcha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace gotcha.Controllers
{
    public class SecondDocController : Controller
    {
        private GuildsEntities db = new GuildsEntities();
        public ViewResult Index()
        {
            return View();
        }
      
        public ActionResult Result(string id_g, string letter, string currentFilter, string currentFilter1, int? pg )
        {

            if (letter != null || id_g !=null)
            {
                pg = 1;
            }
            else
            {
                id_g = currentFilter;
                letter = currentFilter1;
            }
            ViewBag.currentFilter = id_g;
            ViewBag.currentFilter1 = letter;
            
            if ((String.IsNullOrEmpty(letter) || String.IsNullOrEmpty(id_g)) && pg == 1)
            {
                TempData["msg"] = "1";
                return View();
            }
            else
            {
                TempData["msg"] = "";
            }
            int d1;
            try {  
                d1 = Convert.ToInt32(id_g);
            }
            catch
            {
                ModelState.AddModelError("", "try another number of guild");
                TempData["msg"] = "try another number of guild";
                return View();
            }

            
            object[] parameters = {
                new SqlParameter("@id_g",SqlDbType.Int) {Value=d1},
                new SqlParameter("@letter",SqlDbType.VarChar) {Value=letter}
            };
          
            db.Database.CommandTimeout = 360;
            List<SecondDoc_Result> requests = db.Database.SqlQuery<SecondDoc_Result>("SecondDoc @id_g, @letter", parameters).ToList();
            int pageSize = 20;
            int pageNumber = (pg ?? 1);
            return View(requests.ToPagedList(pageNumber, pageSize));
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