using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NovelWeb_SS5.Models;

namespace NovelWeb_SS5.Controllers
{
    public class CommentController : Controller
    {
        private DbModelsNovelWeb db = new DbModelsNovelWeb();

        // GET: Comment
        public ActionResult Index()
        {
            List<Comment> cmt = db.Comments.ToList();
            return View(cmt);
        }

        public ActionResult PostComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostComment(string CommentText)
        {
            int userID = Convert.ToInt32(Session["UserId"]);
            if (userID == 0)
            {
                return RedirectToAction("IndexUser", "LoginUser");
            }

            Comment c = new Comment();
            c.noiDungcmt = CommentText;
            c.ngay = DateTime.Now;
            c.idUser = userID;
            db.Comments.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PostReply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostReply(string CommentText)
        {
            int userID = Convert.ToInt32(Session["UserId"]);
            string userName = Convert.ToString(Session["UserName"]);
            Comment c = new Comment();
            c.noiDungcmt = CommentText;
            c.ngay = DateTime.Now;

            c.userName = userName;
            db.Comments.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}