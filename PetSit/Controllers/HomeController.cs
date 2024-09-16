using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetSit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Initialize UserManager in the action method
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Get the current user's ID
            var userId = User.Identity.GetUserId();

            // Fetch the full ApplicationUser object using the UserManager
            var user = userManager.FindById(userId);

            // Pass the user object to the view using ViewBag
            ViewBag.User = user;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Dashboard()
        {

            return View(Session["user"]);
        }
    }
}