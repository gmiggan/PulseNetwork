using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PulseNetwork.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Data.Entity;
using Newtonsoft.Json;
using PulseNetwork.Utils;
using System.Net;

namespace PulseNetwork.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        BusinessLogic bl = new BusinessLogic();
        String userID;
        
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
                Profile profile = db.Profiles.Find(id);
                ViewBag.DataPoints = JsonConvert.SerializeObject(skillchart(id));
            
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }
        
        public ActionResult Edit(String id)
        {
            Profile profile = db.Profiles.Find(id);
            return View(profile);
        }



        // POST: /Profile/Edit
        [HttpPost]
        public ActionResult Edit(Profile profile)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                
                string redirectString = "View Profile" + profile.UserID + "";
                return RedirectToAction("ViewProfile", new {id = profile.UserID});
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

        public ActionResult AjaxJSON()
        {
            //ViewBag.DataPoints = new DataContractJsonSerializer(dataPoints.GetType()).;
            ViewBag.DataPoints = JsonConvert.SerializeObject("");

            return View();
        }

        public ContentResult JSONData()
        {
            return Content(JsonConvert.SerializeObject(""), "application/json");
        }

        public List<DataPoint> skillchart(string id)
        {
            var skills = bl.UserSkillList(id);
            List<DataPoint> _dataPoints = new List<DataPoint>();
            foreach (var skill in skills)
            {
                var dp = new DataPoint(skill.getSkillName(), skill.Score);
                _dataPoints.Add(dp);
            }

            return _dataPoints;
        }

    }


}