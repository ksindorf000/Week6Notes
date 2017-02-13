using Day1AttrRoutes.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Day1AttrRoutes.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users
                .Where(u => u.Id == userId) //Returns a collection
                .FirstOrDefault(); //Returns only one result
            return View(currentUser);
        }


        // DETAILS: User
        //Using an Attribute Route
        //Route(pattern)
        [Route("User/{userName}")]
        public ActionResult Detail(string userName)
        {
            ApplicationUser currentUser = db.Users
               .Where(u => u.UserName == userName) 
               .FirstOrDefault();
            return View(currentUser);
        }

    }
}