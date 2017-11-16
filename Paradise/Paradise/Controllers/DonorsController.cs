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
    public class DonorsController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        // GET: Donors
        public ActionResult Index()
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                return View(db.Donors.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Donors/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Donor donor = db.Donors.Find(id);
                if (donor == null)
                {
                    return HttpNotFound();
                }
                return View(donor);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Donors/Create
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

        // POST: Donors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "donor_ID,donorName,donorLevel,donorYear,phone,email,active")] Donor donor)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Donors.Add(donor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(donor);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Donors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Donor donor = db.Donors.Find(id);
                if (donor == null)
                {
                    return HttpNotFound();
                }
                return View(donor);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "donor_ID,donorName,donorLevel,donorYear,phone,email,active")] Donor donor)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(donor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(donor);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Donors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Donor donor = db.Donors.Find(id);
                if (donor == null)
                {
                    return HttpNotFound();
                }
                return View(donor);
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

        // POST: Donors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                Donor donor = db.Donors.Find(id);
                db.Donors.Remove(donor);
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
