using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PulseNetwork.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace PulseNetwork.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        
        //GET Profile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            //Profile userprofile = db.Profiles.Find(User.Identity.GetUserId());

            return ViewProfile(User.Identity.GetUserId());
        }

        //GET Profile by User Id
        //Profile/ViewProfile/id
        public ActionResult ViewProfile(string id)
        {

            Profile profile = db.Profiles.Find(id);
            
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: /Profile/Edit
        [HttpPost]
        public ActionResult Edit(Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                string redirectString = "View Profile" + profile.UserID + "";
                return RedirectToAction(redirectString);
            }

            return View(profile);
        }
        [HttpPost]
        public ActionResult Create()
        {
            Profile profile = new Profile();
            if (ModelState.IsValid)
            {
                
                profile.UserID = User.Identity.GetUserId();
                profile.Company = "";
                profile.Bio = "";
                profile.JobTitle = "";
                profile.Location = "";
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("View/" + profile.UserID);
            }

            return View(profile);
        }


    }
}