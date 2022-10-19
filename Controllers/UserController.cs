using BarReporting.Data;
using BiselaWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kweb.Controllers
{
    public class UserController : Controller
    {
        BEntities db;
        User user;
        UserModel userModel;
        // GET:User
        public ActionResult Index()
        {
            List<User> users = new List<User>();
            using (var db = new BEntities())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //var gender = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            using (var db = new BEntities())
            {
                userModel = new UserModel()
                {
                    //roles = db.Roles.Where(x => x.Deleted == 0).ToList(),
                    //shops = db.Shops.Where(x => x.Deleted == 0).ToList(),
                    //userTypes = db.UserTypes.Where(x => x.Deleted == 0).ToList(),
                    //genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList(),
                    //UserTypeId = 0,
                    //RoleId = 0,
                    //ShopId = 0,
                };
            }
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            int UserId = (int)Session["UserId"];
            try
            {
                using (db = new BEntities())
                {
                    user = new User()
                    {
                        UserId = short.Parse(Request.Form["id"]),
                        Name = Request.Form["Name"],
                        UserName = Request.Form["UserName"],
                        Email = Request.Form["Email"],
                        Phone = Request.Form["Phone"],
                        PassWord = Request.Form["Phone"],
                    };

                    if (user.UserId > 0)
                    {
                        db.Entry(user).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Users.Add(user);
                    }
                    db.SaveChanges();
                }
                TempData["successMsg"] = "User Successfully Saved";
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id, UserModel userDetails)
        {
            using (var db = new BEntities())
            {
                var userSingle = db.Users.Where(x => x.UserId == id).SingleOrDefault();
                userDetails = new UserModel()
                {
                    UserName = userSingle.UserName,
                    UserId = userSingle.UserId,
                    Name = userSingle.Name,
                    Email = userSingle.Email,
                    Phone = userSingle.Phone,
                };
            }
            return View("Create", userDetails);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new BEntities())
                {
                    var user = db.Users.Where(x => x.UserId == id).SingleOrDefault();
                    user.Deleted = 1;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                TempData["successMsg"] = "User Successfully Deleted";
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }
    }
}