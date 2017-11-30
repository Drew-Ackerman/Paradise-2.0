using Paradise.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Paradise.Controllers
{
    public class AboutUsController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        // GET: AboutUs
        public ActionResult AboutUs()
        {
            if (Session["userName"]?.ToString() == null)
            {
                Page page = db.Pages.Find(3);
                ViewBag.controllerName = "AboutUs";
                ViewBag.staff = db.Staffs.ToList();
                return View(page);
            }
            else
            {
                return RedirectToAction("Edit", "AboutUs");
            }
        }

        public ActionResult Edit()
        {
            if (Session["userName"]?.ToString() != null)
            {
                Page page = db.Pages.Find(3);
                ViewBag.controllerName = "AboutUs";
                ViewBag.staff = db.Staffs.ToList();
                return View(page);
            }
            else
            {
                return RedirectToAction("Index", "AboutUs");
            }
        }

        [HttpPost]
        public ActionResult Save([Bind(Include = "page_ID,pageName,pageDesc,content")] Page page)
        {
            if (Session["userName"]?.ToString() != null)
            {
                //Remove new lines from the html
                page.content = page.content.Replace("\n", "");

                //Split the data at > characters so we can trim out all of the unnecessary spacing.
                //Using Regular expressions so we can keep the > characters, and because it is very fast.
                string[] splitData = Regex.Split(page.content, @"(?<=[>])");
                page.content = "";

                foreach (string part in splitData)
                {
                    page.content += part.Trim(' ');
                }

                if (ModelState.IsValid)
                {
                    db.Entry(page).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Edit", "AboutUs");
            }
            else
            {
                return RedirectToAction("Index", "AboutUs");
            }

        }
    }
}