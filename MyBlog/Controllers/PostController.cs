using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    //Authorize data annotation requires a user
    //logged in to access anything in this controller
    [Authorize()]
    public class PostController : Controller
    {
        //make a connnection to the database
        Models.MyBlogEntities db =new Models.MyBlogEntities();
        
        
        //
        // GET: /Post/

        public ActionResult Index()
        {
            //the index will return all of the 
            //user's posts
            return View(db.Posts.Where(x => x.Username.ToLower() == User.Identity.Name.ToLower())); 
        }
        // Get: /Post/Create
        [HttpGet]
        public ActionResult Create()
        {
           //pass a blank post object to the view
            return   View(new Models.Post());
        }
        //Post: /Post/Create
        [HttpPost]
        public ActionResult Create(Models.Post post) 
        {
            //set the date create to be Now
            post.Username = User.Identity.Name;
            //set the date create to be Now
            post.DateCreated = DateTime.Now;
            //set the likes to 0
            post.Likes = 0;
            //make sure that imageURL has a value
            if (post.ImageURL == null)
            {
                post.ImageURL = "";
            }
            //add it to the database
            db.Posts.Add(post);
            //save our changes
            db.SaveChanges();
            //kick user back to their list of posts
            return RedirectToAction("Index", "Post");
        }
    //Get: /Post/Delete/(id)
        [HttpGet]
        public ActionResult Delete(int id) 
        {
            Models.Post postToDelete = db.Posts.Find(id);
            //pass the object to the view
            return View(postToDelete);
         }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Post postToDelete = db.Posts.Find(id);
            //delete the post from the database
            db.Posts.Remove(postToDelete);
            //save the changes to the DB
            db.SaveChanges();
            //redirect the user 
            return RedirectToAction("Index", "Post");
        }
    //Get: /Post/Edit/1
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get the post to edit from the db
            Models.Post postToEdit = db.Posts.Find(id);
            //pass our post to edit to the view
            return View(postToEdit);
        }
    //Post: /Post/Edit/1
        [HttpPost]
        public ActionResult Edit(Models.Post postToEdit) 
        {
        //Set the post to be updated
            db.Entry(postToEdit).State = System.Data.EntityState.Modified;
            //save changes
            db.SaveChanges();
            //kick the user back to their post list
            return RedirectToAction("Index", "Post");
        }
    }
}
