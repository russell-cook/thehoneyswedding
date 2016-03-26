using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoneyWedding.DAL;
using HoneyWedding.Models;

namespace HoneyWedding.Controllers
{
    [Authorize(Roles = "WeddingAdmin")]
    public class AccommodationLocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccommodationLocations
        public ActionResult Index()
        {
            return View(db.AccommodationLocations.ToList());
        }

        // GET: AccommodationLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccommodationLocation accommodationLocation = db.AccommodationLocations.Find(id);
            if (accommodationLocation == null)
            {
                return HttpNotFound();
            }
            return View(accommodationLocation);
        }

        // GET: AccommodationLocations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccommodationLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LocationName,PhoneNumber,Email,Website,Notes,HoneyComments,InFairPlay,DistanceFromVenue")] AccommodationLocation accommodationLocation)
        {
            if (ModelState.IsValid)
            {
                db.AccommodationLocations.Add(accommodationLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accommodationLocation);
        }

        // GET: AccommodationLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccommodationLocation accommodationLocation = db.AccommodationLocations.Find(id);
            if (accommodationLocation == null)
            {
                return HttpNotFound();
            }
            return View(accommodationLocation);
        }

        // POST: AccommodationLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LocationName,PhoneNumber,Email,Website,Notes,HoneyComments,InFairPlay,DistanceFromVenue")] AccommodationLocation accommodationLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accommodationLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accommodationLocation);
        }

        // GET: AccommodationLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccommodationLocation accommodationLocation = db.AccommodationLocations.Find(id);
            if (accommodationLocation == null)
            {
                return HttpNotFound();
            }
            return View(accommodationLocation);
        }

        // POST: AccommodationLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccommodationLocation accommodationLocation = db.AccommodationLocations.Find(id);
            db.AccommodationLocations.Remove(accommodationLocation);
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
