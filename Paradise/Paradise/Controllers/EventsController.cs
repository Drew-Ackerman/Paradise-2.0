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
    public class EventsController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        // GET: Events
        public ActionResult Index()
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                return View(db.Events.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Event @event = db.Events.Find(id);
                if (@event == null)
                {
                    return HttpNotFound();
                }
                return View(@event);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Events/Create
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

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "event_ID,eventName,eventDate,eventTime,eventLocation,eventDetails,active")] Event @event)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Events.Add(@event);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(@event);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Event @event = db.Events.Find(id);
                if (@event == null)
                {
                    return HttpNotFound();
                }
                return View(@event);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "event_ID,eventName,eventDate,eventTime,eventLocation,eventDetails,active")] Event @event)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(@event).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(@event);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Event @event = db.Events.Find(id);
                if (@event == null)
                {
                    return HttpNotFound();
                }
                return View(@event);
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

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                Event @event = db.Events.Find(id);
                db.Events.Remove(@event);
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
