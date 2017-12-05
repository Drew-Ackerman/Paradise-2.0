using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Paradise.Models;
using System.Data.Entity;

namespace Paradise.Controllers
{
    public class ProfileImagesController : Controller
    {
        private YFUTEntities db = new YFUTEntities();

        // GET: ProfileImages
        public ActionResult Index()
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                Admin admin = db.Admins.Find(Int32.Parse(Session["adminID"]?.ToString()));
                Staff staff = admin.Staff;
                return View(staff);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

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

        public ActionResult Delete(string file)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (file == null || file == "")
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ViewBag.fileName = file;
                return View();
            }
            else if (Session["isSuperAdmin"]?.ToString() == "False")
            {
                if (file == null || file == "")
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Admin admin = db.Admins.Find(Int32.Parse(Session["adminID"]?.ToString()));
                Staff staff = admin.Staff;

                if (file == staff.imageName)
                {
                    return View();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: ProfileImages/Delete/fileName
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string file)
        {
            if (Session["isSuperAdmin"]?.ToString() == "True")
            {
                if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/profileImages"), file)))
                {
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/profileImages"), file));

                    //find the currently logged in staff member
                    Admin admin = db.Admins.Find(Int32.Parse(Session["adminID"]?.ToString()));
                    Staff staff = admin.Staff;

                    List<Staff> staffMembers = db.Staffs.Where(s => s.imageName == file).ToList();

                    foreach (Staff curStaff in staffMembers) {
                        curStaff.imageName = "default.png";

                        //Change their profile image
                        if (curStaff == staff)
                        {
                            Session["imageSrc"] = "";
                        }

                        if (ModelState.IsValid)
                        {
                            db.Entry(curStaff).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }     
                }
                return RedirectToAction("Index");
            }
            else if (Session["isSuperAdmin"]?.ToString() == "False")
            {

                Admin admin = db.Admins.Find(Int32.Parse(Session["adminID"]?.ToString()));
                Staff staff = admin.Staff;

                if (file == staff.imageName)
                {
                    if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/profileImages"), file)))
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/profileImages"), file));

                        staff.imageName = "default.png";
                        Session["imageSrc"] = "";

                        if (ModelState.IsValid)
                        {
                            db.Entry(staff).State = EntityState.Modified;
                            db.SaveChanges(); 
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase image)
        {
            if (Session["isSuperAdmin"]?.ToString() != null)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/Content/profileImages"), fileName));
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}