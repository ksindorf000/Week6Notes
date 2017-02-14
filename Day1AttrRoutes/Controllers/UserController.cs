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

            var reqId = User.Identity.GetUserId();
            string targetId = currentUser.Id;

            bool isFriend = db.Friends
                .Where(
                f => (f.RequestorId == reqId 
                      && f.TargetId == targetId)
                || 
                     (f.RequestorId == targetId 
                      && f.TargetId == reqId)
                )
                .Any();

            ViewBag.IsFriend = isFriend;

            return View(currentUser);
        }

        //ADD: Friend
        [HttpPost]
        [Route("User/{userName}")]
        public ActionResult AddFriend(string userName)
        {
            //Any business logic (blocked users, spam, hackers) goes here

            var reqId = User.Identity.GetUserId();
            var targetUser = db.Users
                .Where(u => u.UserName == userName)
                .FirstOrDefault();
            string targetId = targetUser.Id;

            Friend relationship = new Friend
            {
                RequestorId = reqId,
                TargetId = targetId
            };

            db.Friends.Add(relationship);
            db.SaveChanges();

            return RedirectToAction("Detail");
        }

    }
}