using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp;

namespace WebApp.Controllers
{
    public class AlertsController : Controller
    {
        private hacaEntities db = new hacaEntities();

        // GET: Alerts
        public ActionResult Index()
        {
            var alerts = db.Alerts.Include(a => a.DevLayout);
            return View(alerts.ToList());
        }

        // GET: Alerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alerts alerts = db.Alerts.Find(id);
            if (alerts == null)
            {
                return HttpNotFound();
            }
            return View(alerts);
        }

        // GET: Alerts/Create
        public ActionResult Create()
        {
            ViewBag.DevLayoutId = new SelectList(db.DevLayout, "dvLtAutoId", "dvLtName");
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Message,DevLayoutId,dateInstert,color,size")] Alerts alerts)
        {
            if (ModelState.IsValid)
            {
                db.Alerts.Add(alerts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DevLayoutId = new SelectList(db.DevLayout, "dvLtAutoId", "dvLtName", alerts.DevLayoutId);
            return View(alerts);
        }

        // GET: Alerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alerts alerts = db.Alerts.Find(id);
            if (alerts == null)
            {
                return HttpNotFound();
            }
            ViewBag.DevLayoutId = new SelectList(db.DevLayout, "dvLtAutoId", "dvLtName", alerts.DevLayoutId);
            return View(alerts);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Message,DevLayoutId,dateInstert,color,size")] Alerts alerts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alerts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DevLayoutId = new SelectList(db.DevLayout, "dvLtAutoId", "dvLtName", alerts.DevLayoutId);
            return View(alerts);
        }

        // GET: Alerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alerts alerts = db.Alerts.Find(id);
            if (alerts == null)
            {
                return HttpNotFound();
            }
            return View(alerts);
        }

        // POST: Alerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alerts alerts = db.Alerts.Find(id);
            db.Alerts.Remove(alerts);
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
