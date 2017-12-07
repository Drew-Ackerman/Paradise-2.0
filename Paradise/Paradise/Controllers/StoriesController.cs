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
    public class StoriesController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        // GET: Stories
        public ActionResult Index()
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                return View(db.Stories.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Stories/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Story story = db.Stories.Find(id);
                if (story == null)
                {
                    return HttpNotFound();
                }
                return View(story);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Stories/Create
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

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "story_ID,storyTitle,storyName,storyContent,active")] Story story)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Stories.Add(story);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(story);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Story story = db.Stories.Find(id);
                if (story == null)
                {
                    return HttpNotFound();
                }
                return View(story);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "story_ID,storyTitle,storyName,storyContent,active")] Story story)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(story).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(story);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Story story = db.Stories.Find(id);
                if (story == null)
                {
                    return HttpNotFound();
                }
                return View(story);
            }
            else if(Session["isSuperAdmin"]?.ToString() == "false") {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            else if (Session["isSuperAdmin"]?.ToString() == "false")
            {
                return RedirectToAction("Index");
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
