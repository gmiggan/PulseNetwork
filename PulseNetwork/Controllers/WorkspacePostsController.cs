using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PulseNetwork.Models;
using Microsoft.AspNet.Identity;

namespace PulseNetwork.Controllers
{
    public class WorkspacePostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkspacePosts
        public ActionResult Index()
        {
            var workspacePosts = db.WorkspacePosts.Include(w => w.poster).Include(w => w.workspace);
            return View(workspacePosts.ToList());
        }

        // GET: WorkspacePosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkspacePost workspacePost = db.WorkspacePosts.Find(id);
            if (workspacePost == null)
            {
                return HttpNotFound();
            }
            return View(workspacePost);
        }

        // GET: WorkspacePosts/Create
        public ActionResult Create(int workspaceid)
        {
            var newPost = new WorkspacePost();
            newPost.workspaceId = workspaceid;

            return PartialView(newPost);
        }

        // POST: WorkspacePosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,workspaceId,posterId,TimePosted,postDetails")] WorkspacePost workspacePost)
        {
            if (ModelState.IsValid)
            {
                
                workspacePost.posterId = User.Identity.GetUserId();
                
                db.WorkspacePosts.Add(workspacePost);
                db.SaveChanges();
                return View(workspacePost);
            }

            ViewBag.posterId = new SelectList(db.Users, "Id", "FullName", workspacePost.posterId);
            ViewBag.workspaceId = new SelectList(db.Workspaces, "id", "userId", workspacePost.workspaceId);
            return View(workspacePost);
        }

        // GET: WorkspacePosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkspacePost workspacePost = db.WorkspacePosts.Find(id);
            if (workspacePost == null)
            {
                return HttpNotFound();
            }
            ViewBag.posterId = new SelectList(db.Users, "Id", "FullName", workspacePost.posterId);
            ViewBag.workspaceId = new SelectList(db.Workspaces, "id", "userId", workspacePost.workspaceId);
            return View(workspacePost);
        }

        // POST: WorkspacePosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,workspaceId,posterId,TimePosted,postDetails")] WorkspacePost workspacePost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workspacePost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.posterId = new SelectList(db.Users, "Id", "FullName", workspacePost.posterId);
            ViewBag.workspaceId = new SelectList(db.Workspaces, "id", "userId", workspacePost.workspaceId);
            return View(workspacePost);
        }

        // GET: WorkspacePosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkspacePost workspacePost = db.WorkspacePosts.Find(id);
            if (workspacePost == null)
            {
                return HttpNotFound();
            }
            return View(workspacePost);
        }

        // POST: WorkspacePosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkspacePost workspacePost = db.WorkspacePosts.Find(id);
            db.WorkspacePosts.Remove(workspacePost);
            db.SaveChanges();
            return RedirectToAction("Index");
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
