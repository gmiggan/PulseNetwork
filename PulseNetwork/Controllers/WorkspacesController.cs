using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PulseNetwork.Models;

namespace PulseNetwork.Controllers
{
    public class WorkspacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Workspaces
        public ActionResult Index()
        {
            var workspaces = db.Workspaces.Include(x => x.creator);
            return View(workspaces.ToList());
        }

        // GET: Workspaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workspace workspace = db.Workspaces.Find(id);
            if (workspace == null)
            {
                return HttpNotFound();
            }
            return View(workspace);
        }

        // GET: Workspaces/Create
        public ActionResult Create()
        {
            ViewBag.creatorID = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Workspaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,creatorID")] Workspace workspace)
        {
            if (ModelState.IsValid)
            {
                db.Workspaces.Add(workspace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.creatorID = new SelectList(db.Users, "Id", "FullName", workspace.creatorID);
            return View(workspace);
        }

        // GET: Workspaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workspace workspace = db.Workspaces.Find(id);
            if (workspace == null)
            {
                return HttpNotFound();
            }
            ViewBag.creatorID = new SelectList(db.Users, "Id", "FullName", workspace.creatorID);
            return View(workspace);
        }

        // POST: Workspaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,creatorID")] Workspace workspace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workspace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.creatorID = new SelectList(db.Users, "Id", "FullName", workspace.creatorID);
            return View(workspace);
        }

        // GET: Workspaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workspace workspace = db.Workspaces.Find(id);
            if (workspace == null)
            {
                return HttpNotFound();
            }
            return View(workspace);
        }

        // POST: Workspaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workspace workspace = db.Workspaces.Find(id);
            db.Workspaces.Remove(workspace);
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
