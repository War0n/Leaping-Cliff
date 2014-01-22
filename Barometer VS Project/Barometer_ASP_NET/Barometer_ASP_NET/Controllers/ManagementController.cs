using Barometer_ASP_NET.Filters;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    [AuthFilter("manager")]
    public class ManagementController : Controller
    {
        //
        // GET: /Management/
        DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

        public ActionResult List()
        {
            IQueryable<User> users = context.Users.Where(u => u.id > 0);
            return View(users);
        }

        //
        // GET: /Management/Details/5

        public ActionResult Details(int id)
        {
            User user = context.Users.Where(u => u.id == id).FirstOrDefault();
            return View(user);
        }

        //
        // GET: /Management/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Management/Create

        [HttpPost]
        public ActionResult Create(User collection)
        {
            try
            {
                context.Users.InsertOnSubmit(collection);
                context.SubmitChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Management/Edit/5

        public ActionResult Edit(int id)
        {
            User user = context.Users.Where(u => u.id == id).FirstOrDefault();
            return View(user);
        }

        //
        // POST: /Management/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, User collection)
        {
            try
            {
                User user = context.Users.Where(u => u.id == id).Single();
                user.id = collection.id;
                user.firstname = collection.firstname;
                user.lastname = collection.lastname;
                user.profile_image = collection.profile_image;
                user.rol_name = collection.rol_name;
                user.student_number = collection.student_number;
                user.email = collection.email;
                context.SubmitChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Management/Delete/5

        public ActionResult Delete(int id)
        {
            User user = context.Users.Where(u => u.id == id).FirstOrDefault();
            context.Users.DeleteOnSubmit(user);
            context.SubmitChanges();
            return RedirectToAction("List");
        }

        //
        // POST: /Management/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
