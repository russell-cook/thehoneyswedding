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
using System.Threading.Tasks;

namespace HoneyWedding.Controllers
{
    [Authorize(Roles = "WeddingAdmin")]
    public class AccommodationRoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccommodationRooms
        public ActionResult Index()
        {
            var accommodationRooms = db.AccommodationRooms.Include(a => a.Location);
            return View(accommodationRooms.ToList());
        }

        // GET: AccommodationRooms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccommodationRoom accommodationRoom = await db.AccommodationRooms.Where(r => r.ID == id).Include(r => r.Location).FirstOrDefaultAsync();
            if (accommodationRoom == null)
            {
                return HttpNotFound();
            }
            return View(accommodationRoom);
        }

        //// GET: AccommodationRooms/Create
        //public ActionResult Create()
        //{
        //    ViewBag.AccommodationLocationID = new SelectList(db.AccommodationLocations, "ID", "LocationName");
        //    return View();
        //}

        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var location = await db.AccommodationLocations.FindAsync(id);
            ViewBag.AccommodationLocationID = new SelectList(db.AccommodationLocations, "ID", "LocationName", location.ID);
            return View(new AccommodationRoom { AccommodationLocationID = location.ID });
        }

        // POST: AccommodationRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AccommodationLocationID,RoomName,SleepsTotal,SleepsBed,SleepsSofa,CostNightly,MinNights,IsAvailable")] AccommodationRoom accommodationRoom)
        {
            if (ModelState.IsValid)
            {
                db.AccommodationRooms.Add(accommodationRoom);
                db.SaveChanges();
                return RedirectToAction("Details", "AccommodationLocations", new { id = accommodationRoom.AccommodationLocationID });
            }

            ViewBag.AccommodationLocationID = new SelectList(db.AccommodationLocations, "ID", "LocationName", accommodationRoom.AccommodationLocationID);
            return View(accommodationRoom);
        }

        // GET: AccommodationRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccommodationRoom accommodationRoom = db.AccommodationRooms.Find(id);
            if (accommodationRoom == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccommodationLocationID = new SelectList(db.AccommodationLocations, "ID", "LocationName", accommodationRoom.AccommodationLocationID);
            return View(accommodationRoom);
        }

        // POST: AccommodationRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AccommodationLocationID,RoomName,SleepsTotal,SleepsBed,SleepsSofa,CostNightly,MinNights,IsAvailable")] AccommodationRoom accommodationRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accommodationRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "AccommodationLocations", new { id = accommodationRoom.AccommodationLocationID });
            }
            ViewBag.AccommodationLocationID = new SelectList(db.AccommodationLocations, "ID", "LocationName", accommodationRoom.AccommodationLocationID);
            return View(accommodationRoom);
        }

        // GET: AccommodationRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccommodationRoom accommodationRoom = db.AccommodationRooms.Find(id);
            if (accommodationRoom == null)
            {
                return HttpNotFound();
            }
            return View(accommodationRoom);
        }

        // POST: AccommodationRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccommodationRoom accommodationRoom = db.AccommodationRooms.Find(id);
            db.AccommodationRooms.Remove(accommodationRoom);
            db.SaveChanges();
            return RedirectToAction("Details", "AccommodationLocations", new { id = accommodationRoom.AccommodationLocationID });
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
