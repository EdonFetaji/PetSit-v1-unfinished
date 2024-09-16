using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PetSit.Models;

namespace PetSit.Controllers
{

    public class PetSittersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        // Property to access UserManager via HttpContext
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            return View(db.PetSitters.ToList());
        }


        public ActionResult RegisterSitter()
        {
            return View();
        }

        // POST: Register Pet Sitter
        [HttpPost]
        public ActionResult RegisterSitter(PetSitter model)
        {
            var user = Session["user"] as ApplicationUser;

            if (User.IsInRole("petSitter"))
            {
                ViewBag.Message = "You are already registered as a PetSitter";
                return View("Error");
            }

            if (string.IsNullOrWhiteSpace(model.availability))
            {
                ModelState.AddModelError("availability", "Availability is required.");
            }

            if (string.IsNullOrWhiteSpace(model.location))
            {
                ModelState.AddModelError("location", "Location is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.rating = 1;

            if (user == null)
            {
                throw new HttpException(404, "No user with this email");
            }

            model.UserID = user.Id;

            // Assign the role 'petSitter' to the user
            UserManager.AddToRole(user.Id, "petSitter");

            db.PetSitters.Add(model);
            db.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }
       


        


        // GET: PetSitters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetSitter petSitter = db.PetSitters.Find(id);
            if (petSitter == null)
            {
                return HttpNotFound();
            }
            return View(petSitter);
        }

        // GET: PetSitters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PetSitters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetSitterID,UserID,offeredServices,availability,location,rating")] PetSitter petSitter)
        {
            if (ModelState.IsValid)
            {
                db.PetSitters.Add(petSitter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(petSitter);
        }

        // GET: PetSitters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetSitter petSitter = db.PetSitters.Find(id);
            if (petSitter == null)
            {
                return HttpNotFound();
            }
            return View(petSitter);
        }

        // POST: PetSitters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetSitterID,UserID,offeredServices,availability,location,rating")] PetSitter petSitter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petSitter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petSitter);
        }

        // GET: PetSitters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetSitter petSitter = db.PetSitters.Find(id);
            if (petSitter == null)
            {
                return HttpNotFound();
            }
            return View(petSitter);
        }

        // POST: PetSitters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetSitter petSitter = db.PetSitters.Find(id);
            db.PetSitters.Remove(petSitter);
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
