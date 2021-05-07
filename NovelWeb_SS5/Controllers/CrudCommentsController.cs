using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NovelWeb_SS5.Models;

namespace NovelWeb_SS5.Controllers
{
    public class CrudCommentsController : Controller
    {
        private DbModelsNovelWeb db = new DbModelsNovelWeb();

        // GET: CrudComments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Chuong).Include(c => c.User);
            return View(comments.ToList());
        }

        // GET: CrudComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: CrudComments/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: CrudComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.idChuong = new SelectList(db.Chuongs, "idChuong", "soChuong", comment.idChuong);
            ViewBag.idUser = new SelectList(db.Users, "idUser", "userName", comment.idUser);
            return View(comment);
        }

        // POST: CrudComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCmt,noiDungcmt,ngay,userName,idChuong,idUser")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idChuong = new SelectList(db.Chuongs, "idChuong", "soChuong", comment.idChuong);
            ViewBag.idUser = new SelectList(db.Users, "idUser", "userName", comment.idUser);
            return View(comment);
        }

        // GET: CrudComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: CrudComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index_Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Index_Admin()
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
            return RedirectToAction("Index_Admin");
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
            return RedirectToAction("Index_Admin");
        }
    }
}