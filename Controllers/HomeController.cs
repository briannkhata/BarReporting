using BarReporting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiselaWeb.Controllers
{
    public class HomeController : Controller
    {
        BEntities db;

        public ActionResult Index()
        {
            using (db = new BEntities()) 
            {
                ViewBag.produts = db.Products.Where(x => x.Deleted == 0).Count();
                ViewBag.categories = db.Categories.Where(x => x.Deleted == 0).Count(); 
                ViewBag.brands = db.Brands.Where(x => x.Deleted == 0).Count();
                ViewBag.customers = db.Users.Where(x=>x.UserType == "Customer").Count();
                ViewBag.suppliers = db.Users.Where(x => x.UserType == "Supplier").Count();
                ViewBag.branches = db.Branches.Where(x => x.Deleted == 0).Count(); 
                //ViewBag.produts = db.Products.Where(x => x.Deleted == 0).Count();
                //ViewBag.produts = db.Products.Where(x => x.Deleted == 0).Count(); 
                //ViewBag.produts = db.Products.Where(x => x.Deleted == 0).Count();
                //ViewBag.produts = db.Products.Where(x => x.Deleted == 0).Count(); 
                //ViewBag.produts = db.Products.Where(x => x.Deleted == 0).Count();

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}