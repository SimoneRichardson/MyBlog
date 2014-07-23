using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; // add using for Memberships

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        //add a connection to the myBlog database
        Models.MyBlogEntities db = new Models.MyBlogEntities();
        // GET: /Account/

        public ActionResult Index()
        {
            //to create a user
            Membership.CreateUser("admin", "techIsFun1");

            //To check if the username and password match
            if (Membership.ValidateUser("admin", "techIsFun1"))
            {
                //user pass is a match, log them in
                FormsAuthentication.SetAuthCookie("admin", false);


            }
            return View();
        }
        //Get://Account/Register
        public ActionResult Register()
        {   //creating a blank registration model 
            //to pass to our view
            return View(new Models.Register());
        }
        //Post: Account/Register
        [HttpPostAttribute]
        public ActionResult Register(Models.Register reg)
        {
            //post action receives a Register object
            //1. check to see if the username is already in use
            if (Membership.GetUser(reg.Username) == null)
            {
                //username is valid, so add to the database
                Membership.CreateUser(reg.Username, reg.Password);
                //add the user to our myBlog authors table
                Models.Author author = new Models.Author();
                author.Username = reg.Username;
                author.Name = reg.Name;
                author.ImageURL = reg.ImageURL;
                //add the author to the database
                db.Authors.Add(author);
                db.SaveChanges();
                //Log the user in
                FormsAuthentication.SetAuthCookie(reg.Username, false);
                //
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //username is already in use, send a message
                //to the view to let the user know
                ViewBag.Error = "Username is aleready in use, good";
                //return the view with the reg object
                return View(reg);
            }


        }
        //Get: //Account/Logout
        [HttpGet]
        public ActionResult Signout()
        {
            //log out the user
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            //log in the user
            return View(new Models.Login());
        }
        [HttpPost]
        public ActionResult LogIn(Models.Login Log)
        {
            if (Membership.ValidateUser(Log.Username, Log.Password))
            {
                FormsAuthentication.SetAuthCookie(Log.Username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Incorrrrect Password. Try again";
                return View(Log);
            }
        }
    }
}







