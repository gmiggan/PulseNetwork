using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PulseNetwork.Models;
using System.Net;
using Microsoft.AspNet.Identity;
using PulseNetwork.Utils;

namespace PulseNetwork.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public BusinessLogic bl = new BusinessLogic();
        

        public ActionResult Index(string show)
        {
            if(show == "available")
            {
                return View(bl.availableQuestions(User.Identity.GetUserId()).OrderByDescending(x => x.DatePosted));
            }
            
            return View(db.Questions.OrderByDescending(x => x.DatePosted).ToList());
            
        }

        // GET: /Questions/Details/id
        public ActionResult Details(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: /Questions/Create
        public ActionResult Create()
        {
            return View(new Question());
        }

        // POST: /Questions/Create
        [HttpPost]
        public ActionResult Create(Question question, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                question.UserID = User.Identity.GetUserId();
                question.DatePosted = DateTime.Today;
                question.TimePosted = DateTime.Now.TimeOfDay;
                db.Questions.Add(question);
                db.SaveChanges();

                String tags = Request.Form["search"];
                List<string> skills = tags.Split(',').ToList<string>();
                foreach (string skill in skills)
                {
                    Skill sk =  question.getSkillIDByName(skill);
                    if (sk != null)
                    {
                        QuestionSkill qs = new QuestionSkill();
                        qs.QuestionID = question.ID;
                        qs.SkillID = sk.ID;
                        db.QuestionSkills.Add(qs);
                        db.SaveChanges();
                    }

                }
                
                return RedirectToAction("Index");
            }

            return View(question);
        }

        public ActionResult Answer(Question Question, Answer answer)
        {
            Question question = db.Questions.Find(Question.ID);
            question.Answers.Add(answer);
            db.Answers.Add(answer);
            db.SaveChanges();


            return View(question);
        }
        

        //GET: /Questions/ViewQuestion/id
        
        public ActionResult ViewQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }
    }
}