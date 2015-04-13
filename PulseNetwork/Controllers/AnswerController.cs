using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PulseNetwork.Models;
using Microsoft.AspNet.Identity;

namespace PulseNetwork.Controllers
{
    public class AnswerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Answer
        public ActionResult Index()
        {
            return View(db.Answers.ToList());
        }

        // GET: /Answers/Details/id
        public ActionResult Details(int id)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: /Answers/Create
        public ActionResult Create(int questionId)
        {
            var newAnswer = new Answer();
            newAnswer.QuestionID = questionId; 

            return PartialView(newAnswer);
        }

        // POST: /Answers/Create
        [HttpPost]
        public ActionResult Create(Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.UserID = User.Identity.GetUserId();
                db.Answers.Add(answer);
                db.SaveChanges();
                return PartialView(answer);
            }

            return PartialView(answer);
        }
    }
}