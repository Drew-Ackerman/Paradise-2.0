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
    public class StaffsController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        // GET: Staffs
        public ActionResult Index()
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                return View(db.Staffs.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Staff staff = db.Staffs.Find(id);
                if (staff == null)
                {
                    return HttpNotFound();
                }
                return View(staff);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "staff_ID,firstName,lastName,jobTitle,position,foundation,email,active,imageName")] Staff staff)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Staffs.Add(staff);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(staff);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Staff staff = db.Staffs.Find(id);
                if (staff == null)
                {
                    return HttpNotFound();
                }
                return View(staff);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "staff_ID,firstName,lastName,jobTitle,position,foundation,email,active,imageName")] Staff staff)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(staff).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(staff);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Staff staff = db.Staffs.Find(id);
                if (staff == null)
                {
                    return HttpNotFound();
                }
                return View(staff);
            }
            else if(Session["isSuperAdmin"]?.ToString() == "False")
            {
                return RedirectToAction("Edit", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                Staff staff = db.Staffs.Find(id);
                db.Staffs.Remove(staff);
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
