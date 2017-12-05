using Paradise.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.WebPages;
using System.Text.RegularExpressions;

namespace Paradise.Controllers
{
    public class HomeController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        public ActionResult Index()
        {
            if (Session["userName"]?.ToString() == null)
            {
                Page page = db.Pages.Where(p => p.pageName == "Home").ToList()[0];
                return View(page);
            }
            else
            {
                return RedirectToAction("Edit", "Home");
            }
        }

        public ActionResult Edit()
        {
            if (Session["userName"]?.ToString() != null)
            {
                Page page = db.Pages.Where(p => p.pageName == "Home").ToList()[0];
                ViewBag.controllerName = "Home";
                return View(page);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Save([Bind(Include = "page_ID,pageName,pageDesc,content")] Page page)
        {
            if (Session["userName"]?.ToString() != null)
            {
                //Remove new lines from the html
                page.content = page.content.Replace(Environment.NewLine, "");

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
                return RedirectToAction("Edit", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}