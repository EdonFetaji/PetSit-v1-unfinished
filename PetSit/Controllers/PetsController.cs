using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PetSit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetSit.Controllers
{
    public class PetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult RegisterPet()
        {
            // Instantiate the view model and pass it to the view
            var model = new PetViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterPet(PetViewModel model)
        {
            var user = Session["user"] as ApplicationUser;
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            // Manual validation
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "Pet's name is required.");
            }

            if (string.IsNullOrEmpty(model.Type))
            {
                ModelState.AddModelError("Type", "Pet's type is required.");
            }

            if (string.IsNullOrEmpty(model.MedicalInfo))
            {
                ModelState.AddModelError("MedicalInfo", "Medical information is required.");
            }

            // Check if there are validation errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Handle profile picture upload
            if (model.ProfilePicture != null && model.ProfilePicture.ContentLength > 0)
            {
                string fileName = Path.GetFileName(model.ProfilePicture.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/PetProfilePictures"), fileName);

                // Save the file and store the path
                model.ProfilePicture.SaveAs(path);
                model.ProfilePicturePath = "/Content/PetProfilePictures/" + fileName;
            }
            else
            {
                model.ProfilePicturePath = "/Content/PetProfilePictures/placeholder.jpg";
            }

            // Create a new Pet object to save to the database
            var newPet = new Pet();

            newPet.type = model.Type;
            newPet.OwnerID = user.Id;
            newPet.medicalInfo = model.MedicalInfo;
            newPet.bio = model.Bio;
            newPet.ProfilePicturePath = model.ProfilePicturePath;
            newPet.name = model.Name;


            db.Pets.Add(newPet);
            db.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }
    }
}