using BarReporting.Data;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Kweb.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        BEntities db;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session.Clear();
            //RestClientEx.Execute<AuthenticationResult>(Urls.Logout, new UserDto() { Username = User.Identity.Name });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login()
        {
            var username = Request.Form["Username"];
            var password = Request.Form["Password"];

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    using (db = new BEntities())
                    {
                        var user = db.Users.Where(x => x.UserName == username && x.PassWord == password && x.Deleted == 0).ToList();
                        if (user.Count == 1)
                        {
                            Session["UserName"] = db.Users.Where(x => x.UserName == username && x.PassWord == password).Single().UserName;
                            Session["UserId"] = db.Users.Where(x => x.UserName == username && x.PassWord == password).Single().UserId;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["userErrorMsg"] = "Wrong Username or Password";
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }

}