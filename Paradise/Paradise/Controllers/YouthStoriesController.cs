﻿using Paradise.Models;
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
    public class YouthStoriesController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        // GET: YouthStories
        public ActionResult YouthStories()
        {
            if (Session["userName"]?.ToString() == null)
            {
                Page page = db.Pages.Where(p => p.pageName == "YouthStories").ToList()[0];
                ViewBag.controllerName = "YouthStories";
                ViewBag.stories = db.Stories.ToList();
                return View(page);
            }
            else
            {
                return RedirectToAction("Edit", "YouthStories");
            }
        }

        public ActionResult Edit()
        {
            if (Session["userName"]?.ToString() != null)
            {
                Page page = db.Pages.Where(p => p.pageName == "YouthStories").ToList()[0];
                ViewBag.controllerName = "YouthStories";
                ViewBag.stories = db.Stories.ToList();
                return View(page);
            }
            else
            {
                return RedirectToAction("YouthStories", "YouthStories");
            }
        }

        [HttpGet]
        public ActionResult getStoryJson(string title)
        {
            Story selectedStory = db.Stories.Where(s => s.storyTitle == title).ToList()[0];
            string html = "<h2>" + selectedStory.storyName + "</h2><p class=\"col-xs-12\">" + selectedStory.storyContent + "</p>";
            JsonResult json = new JsonResult();
            json.Data = new { html =  html};
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
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
                return RedirectToAction("Edit", "YouthStories");
            }
            else
            {
                return RedirectToAction("YouthStories", "YouthStories");
            }

        }
    }
}