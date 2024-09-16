using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PetSit.Models;
using PagedList;
using Utilities;
using Newtonsoft.Json;


namespace PetSit.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public JsonResult GetSitterServices(int id)
        {
            PetSitter result = db.PetSitters.Where(z => z.PetSitterID == id).FirstOrDefault();

            if (result != null && !string.IsNullOrEmpty(result.offeredServices))
            {
                var services = JsonConvert.DeserializeObject<List<Service>>(result.offeredServices);

                return Json(new { success = true, services = services }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, message = "PetSitter not found or no services available" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Bookings
        public ActionResult Index(int? page)
        {
            var user = Session["user"] as ApplicationUser;
            // Fetch all bookings
            var bookings = db.Bookings.Where(b => b.UserID == user.Id).Include(b => b.Pet).Include(b => b.PetSitter)
                .Include(b => b.User)
                .ToList();

            // Set page size
            int pageSize = 6; // Number of items to display per page
            int pageNumber = (page ?? 1); // If no page is specified, show the first page

            // Use PagedList to paginate the bookings
            var pagedBookings = bookings.ToPagedList(pageNumber, pageSize);

            return View(pagedBookings); // Pass the paged data to the view
        }


        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            var user = Session["user"] as ApplicationUser;

            // Selecting a combination of fields to show in the dropdown
            var petSitters = db.PetSitters.Select(s => new
            {
                PetSitterID = s.PetSitterID,
                Description = s.User.Name + " (" + s.location + ")"
            }).ToList();

            ViewBag.pets = db.Pets.Where(b => b.OwnerID == user.Id).ToList();
            ViewBag.sitters = petSitters;


            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "BookingID,PetSitterID,PetID,start,end,comment,status")]
            Booking booking)
        {
            var user = Session["user"] as ApplicationUser;
            var pickedSitter = SessionUtility.GetFlash("PetSitter") as PetSitter;

            // If a sitter is picked and stored in session, use its ID
            if (pickedSitter != null)
            {
                booking.PetSitterID = pickedSitter.PetSitterID;
            }

            // Ensure the user is assigned if needed
            if (user == null)
            {
                ModelState.AddModelError("", "User is not logged in.");
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                booking.UserID = user.Id;
                booking.status = "Pending";

                db.Bookings.Add(booking);
                db.SaveChanges();

                SessionUtility.Flash("PetSitter", null);

                return RedirectToAction("Index");
            }

            var petSitters = db.PetSitters.Select(s => new
            {
                PetSitterID = s.PetSitterID,
                Description = s.User.Name + " (" + s.location + ")"
            }).ToList();
            ViewBag.pets = db.Pets.Where(b => b.OwnerID == user.Id).ToList();
            ViewBag.sitters = petSitters;

            return View(booking);
        }


        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            ViewBag.PetID = new SelectList(db.Pets, "PetID", "name", booking.PetID);
            ViewBag.PetSitterID = new SelectList(db.PetSitters, "PetSitterID", "offeredServices", booking.PetSitterID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "BookingID,PetSitterID,PetID,start,end,comment,status")]
            Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PetID = new SelectList(db.Pets, "PetID", "name", booking.PetID);
            ViewBag.PetSitterID = new SelectList(db.PetSitters, "PetSitterID", "offeredServices", booking.PetSitterID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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