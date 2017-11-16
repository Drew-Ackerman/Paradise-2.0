using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Paradise.Models;

namespace Paradise.Controllers
{
    public class AdminsController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        // GET: Admins
        public ActionResult Index()
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                var admins = db.Admins.Include(a => a.Staff);
                return View(admins.ToList());
            }
            else if(Session["isSuperAdmin"]?.ToString() == "0")
            {
                return RedirectToAction("Edit", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else if (Session["isSuperAdmin"]?.ToString() == "False")
            {
                return RedirectToAction("Edit", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                ViewBag.staff_ID = new SelectList(db.Staffs, "staff_ID", "firstName");
                return View();
            }
            else if (Session["isSuperAdmin"]?.ToString() == "False")
            {
                return RedirectToAction("Edit", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "admin_ID,staff_ID,userName,userPassword,superAdmin")] Admin admin)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (ModelState.IsValid)
                {
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.staff_ID = new SelectList(db.Staffs, "staff_ID", "firstName", admin.staff_ID);
                return View(admin);
            }
            else if (Session["isSuperAdmin"]?.ToString() == "False")
            {
                return RedirectToAction("Edit", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                ViewBag.staff_ID = new SelectList(db.Staffs, "staff_ID", "firstName", admin.staff_ID);
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "admin_ID,staff_ID,userName,userPassword,superAdmin")] Admin admin)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Session["isSuperAdmin"]?.ToString() == "True")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Edit", "Home");
                    }
                }
                ViewBag.staff_ID = new SelectList(db.Staffs, "staff_ID", "firstName", admin.staff_ID);
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else if (Session["isSuperAdmin"]?.ToString() == "False")
            {
                return RedirectToAction("Edit", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                Admin admin = db.Admins.Find(id);
                db.Admins.Remove(admin);
                db.SaveChanges();
            return RedirectToAction("Index");
            }
            else if (Session["isSuperAdmin"]?.ToString() == "False")
            {
                return RedirectToAction("Edit", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
